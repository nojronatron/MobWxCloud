using MobWx.Lib.Common;
using MobWx.Lib.Models;
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
    config.Timeout = TimeSpan.FromSeconds(int.Parse(httpCtsTimeout));
});

// fetch data from extracted NWS urls
builder.Services.AddHttpClient("NwsElementUrl", config =>
{
    config.DefaultRequestHeaders.Add("Accept", httpAcceptHeader);
    config.DefaultRequestHeaders.Add("User-Agent", httpUserAgent);
    config.Timeout = TimeSpan.FromSeconds(int.Parse(httpCtsTimeout));
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
    using (var scope = app.Services.CreateScope())
    {
        var nwsApiAbstraction = scope.ServiceProvider.GetRequiredService<INwsApiAbstraction>();
        Observation currentCondition = await nwsApiAbstraction.GetCurrentConditionsAsync(Position.Create(lat.ToString(), lon.ToString()));
        return Results.Ok(currentCondition);
    }
});

// get 7-day forecast from office nearest to lat, lon
app.MapGet("/api/v1/forecast/{lat:float},{lon:float}", (HttpRequest request) =>
{

});

// get alert(s) in the current zone given lat, lon
app.MapGet("/api/v1/alerts/{lat:float},{lon:float}", (HttpRequest request) =>
{

});

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
