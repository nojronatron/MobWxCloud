using MobWx.API.Endpoints;
using MobWx.API.Models;
using MobWx.Lib.Common;
using MobWx.Lib.Models;
using MobWx.Lib.Models.Base;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var httpClientBaseUrl = builder.Configuration["HttpClient:WeatherApiAddress"] ?? "localhost";
var httpUserAgent = builder.Configuration["HttpClient:UserAgentHeader"] ?? null;
var httpAcceptHeader = builder.Configuration["HttpClient:AcceptHeader"] ?? null;
var httpCtsTimeout = builder.Configuration["HttpClient:CancelTokenTimeout"] ?? "2000";

Debug.WriteLine($"httpClientBaseUrl: {httpClientBaseUrl}\thttpUserAgent: {httpUserAgent}\thttpAcceptHeader: {httpAcceptHeader}\thttpCtsTimeout: {httpCtsTimeout}");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// originate specific endpoint calls to NWS API
builder.Services.AddHttpClient("NwsApi", config =>
{
    config.BaseAddress = new Uri(httpClientBaseUrl);
    config.DefaultRequestHeaders.Add("Accept", httpAcceptHeader);
    config.DefaultRequestHeaders.Add("User-Agent", httpUserAgent);
    config.Timeout = TimeSpan.FromMilliseconds(int.Parse(httpCtsTimeout));
});

// fetch data from extracted NWS urls
builder.Services.AddHttpClient("NwsElementUrl", config =>
{
    config.DefaultRequestHeaders.Add("Accept", httpAcceptHeader);
    config.DefaultRequestHeaders.Add("User-Agent", httpUserAgent);
    config.Timeout = TimeSpan.FromMilliseconds(int.Parse(httpCtsTimeout));
});

builder.Services.AddTransient<INwsApiAbstraction, NwsApiAbstraction>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/* 
 * begin endpoints definitions 
 */

// get current weather conditions from office nearest to lat, lon
app.MapGet("/api/v1/conditions/{lat:float},{lon:float}", async (float lat, float lon, HttpContext httpContext) =>
{
    var position = PositionBase.Create(lat.ToString(), lon.ToString());

    if (position is NullPosition)
    {
        return Results.BadRequest("Invalid latitude (lat) or langitude (lon) values.");
    }

    try
    {
        using IServiceScope scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var nwsApiAbstraction = scope.ServiceProvider.GetRequiredService<INwsApiAbstraction>();
        logger.LogInformation("Fetching current conditions for (lat, lon): ({lat}, {lon}).", lat, lon);

        Observation currentCondition = await nwsApiAbstraction.GetCurrentConditionsAsync(position);

        if (currentCondition is null)
        {
            logger.LogWarning("No current conditions found for (lat, lon): ({lat}, {lon}).", lat, lon);
            return Results.NotFound("No current conditions found.");
        }

        // convert currentCondition into client-friendly JSON
        CurrentObservation currentObservation = CurrentObservation.Create(currentCondition);
        logger.LogInformation("Returning current observation to web requestor with timestamp {currObsTimestamp}", currentObservation.Timestamp);
        return Results.Ok(currentObservation);
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while fetching current conditions for lat: {lat}, lon: {lon}", lat, lon);
        return Results.Problem("An error occurred while processing your request.");
    }
}).WithName("Conditions");

// get 7-day forecast from office nearest to lat, lon
app.MapGet("/api/v1/forecast/{lat:float},{lon:float}", (HttpRequest request) =>
{

}).WithName("Forecast");

// get alert(s) in the current zone given lat, lon
app.MapGet("/api/v1/alerts/{lat:float},{lon:float}", async (float lat, float lon) =>
{
    return await AlertsHandlers.GetActiveAlertsAsync(lat, lon, app);
}).WithName("Alerts");

app.MapGet("/health", () =>
{
    return "Healthy";
})
.WithName("Health")
.WithOpenApi();

/* 
 * end endpoints definitions 
 */

app.Run();
