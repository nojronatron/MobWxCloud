/*
 * MIT License

    Copyright (c) 2025 Jon Rumsey

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

using Microsoft.AspNetCore.Mvc;
using MobWx.API.Common;
using MobWx.API.Endpoints;
using MobWx.Lib.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// common httpclient config
var httpUserAgent = builder.Configuration["HttpClient:UserAgentHeader"] ?? null;
var httpCtsTimeout = builder.Configuration["HttpClient:CancelTokenTimeout"] ?? "2000";

// nws httpclient config
var httpClientBaseNwsUrl = builder.Configuration["HttpClient:WeatherApiAddress"] ?? "localhost";
var httpAcceptHeaderNws = builder.Configuration["HttpClient:AcceptHeaderNws"] ?? null;

// osm httpclient config
var httpClientBaseOsmUrl = builder.Configuration["HttpClient:OpenStreetMapsApiAddress"] ?? null;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// http client originates specific endpoint calls to NWS API
builder.Services.AddHttpClient("NwsApi", config =>
{
    config.BaseAddress = new Uri(httpClientBaseNwsUrl);
    config.DefaultRequestHeaders.Add("Accept", httpAcceptHeaderNws);
    config.DefaultRequestHeaders.Add("User-Agent", httpUserAgent);
    config.Timeout = TimeSpan.FromMilliseconds(int.Parse(httpCtsTimeout));
});

// http client fetches data from extracted NWS urls
builder.Services.AddHttpClient("NwsElementUrl", config =>
{
    config.DefaultRequestHeaders.Add("Accept", httpAcceptHeaderNws);
    config.DefaultRequestHeaders.Add("User-Agent", httpUserAgent);
    config.Timeout = TimeSpan.FromMilliseconds(int.Parse(httpCtsTimeout));
});

// http client originates specific endpoing calls to OpenStreetMaps API
builder.Services.AddHttpClient("OsmApi", config =>
{
    config.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");
    config.DefaultRequestHeaders.Add("User-Agent", httpUserAgent);
    config.Timeout = TimeSpan.FromMilliseconds(int.Parse(httpCtsTimeout));
});

builder.Services.AddTransient<ICurrentConditionsHandler, CurrentConditionsHandler>();
builder.Services.AddTransient<INwsEndpointAbstraction, NwsEndpointAbstraction>();
builder.Services.AddTransient<IJsonHandler, JsonHandler>();
builder.Services.AddTransient<IForecastsHandler, ForecastsHandler>();
builder.Services.AddTransient<IAlertsHandler, AlertsHandler>();
builder.Services.AddTransient<ILocationHandler, LocationHandler>();
builder.Services.AddTransient<IOpenStreetMapsAbstraction, OpenStreetMapsAbstraction>();

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

// get geocoded lat and lon from city, state (continuous US plus Alaska and Hawaii)
app.MapGet("/api/v1/location/{city},{state}",
    async (
        [FromRoute] string city,
        [FromRoute] string state,
        [FromServices] ILocationHandler locationHandler
        )
    =>
    {
        return await locationHandler.GetGeoLocationAsync(city, state);
    }).WithName("Location");

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
app.MapGet("/api/v1/forecast/{latitude:double},{longitude:double}",
    async (
        [FromRoute]double latitude, 
        [FromRoute]double longitude,
        [FromServices] IForecastsHandler forecastsHandler) =>
{
    var position = new Coordinate(latitude, longitude).ToPosition();
    return await forecastsHandler.GetForecastsAsync(position);
}).WithName("Forecast");

// get alert(s) in the current zone given lat, lon
app.MapGet("/api/v1/alerts/{latitude:double},{longitude:double}", 
    async (
        [FromRoute]double latitude, 
        [FromRoute]double longitude,
        [FromServices] IAlertsHandler alertsHandler) =>
{
    var position = new Coordinate(latitude, longitude).ToPosition();
    return await alertsHandler.GetActiveAlertsAsync(position);
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
