using Microsoft.AspNetCore.Mvc;
using MobWx.API.Common;
using MobWx.API.Endpoints;
using MobWx.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var httpClientBaseUrl = builder.Configuration["HttpClient:WeatherApiAddress"] ?? "localhost";
var httpUserAgent = builder.Configuration["HttpClient:UserAgentHeader"] ?? null;
var httpAcceptHeader = builder.Configuration["HttpClient:AcceptHeader"] ?? null;
var httpCtsTimeout = builder.Configuration["HttpClient:CancelTokenTimeout"] ?? "2000";

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

builder.Services.AddTransient<ICurrentConditionsHandler, CurrentConditionsHandler>();
builder.Services.AddTransient<INwsEndpointAbstraction, NwsEndpointAbstraction>();
builder.Services.AddTransient<IJsonHandler, JsonHandler>();
builder.Services.AddTransient<IForecastsHandler, ForecastsHandler>();

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
app.MapGet("/api/v1/conditions/{latitude:double},{longitude:double}", 
    async (
        [FromRoute]double latitude, 
        [FromRoute]double longitude,
        [FromServices] ICurrentConditionsHandler currentConditionsHandler
    ) =>
{
    var position = new Coordinate(latitude, longitude).ToPosition();
    return await currentConditionsHandler.GetCurrentConditionsAsync(position);
}).WithName("Conditions");

// get 7-day forecast from office nearest to lat, lon
app.MapGet("/api/v1/forecast/{latitude:float},{longitude:float}",
    async (
        [FromRoute]float latitude, 
        [FromRoute]float longitude,
        [FromServices] IForecastsHandler forecastsHandler) =>
{
    var position = new Coordinate(latitude, longitude).ToPosition();
    return await forecastsHandler.GetForecastsAsync(position);
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
