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
using MobWx.API.ServerConfig;
using MobWx.Lib.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

HttpClientSettings.ConfigureHttpClients(builder);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

ServicesConfig.ConfigureServices(builder);

builder.Services.AddSwaggerGen();

var app = builder.Build();

AppConfig.ConfigureApp(app);

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
        [FromRoute] double latitude,
        [FromRoute] double longitude,
        [FromServices] ICurrentConditionsHandler currentConditionsHandler
    ) =>
    {
        var position = new Coordinate(latitude, longitude).ToPosition();
        return await currentConditionsHandler.GetCurrentConditionsAsync(position);
    }).WithName("Conditions");

// get 7-day forecast from office nearest to lat, lon
app.MapGet("/api/v1/forecast/{latitude:double},{longitude:double}",
    async (
        [FromRoute] double latitude,
        [FromRoute] double longitude,
        [FromServices] IForecastsHandler forecastsHandler) =>
    {
        var position = new Coordinate(latitude, longitude).ToPosition();
        return await forecastsHandler.GetForecastsAsync(position);
    }).WithName("Forecast");

// get alert(s) in the current zone given lat, lon
app.MapGet("/api/v1/alerts/{latitude:double},{longitude:double}",
    async (
        [FromRoute] double latitude,
        [FromRoute] double longitude,
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
