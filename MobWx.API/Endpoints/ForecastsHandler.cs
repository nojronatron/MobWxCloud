using MobWx.Lib.ForecastModels;
using MobWx.Lib.Models;
using MobWx.Lib.Models.Base;
using MobWx.Lib.PointModels;
using System.Text.Json;

namespace MobWx.API.Endpoints;

public class ForecastsHandler
{
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Get the relative path for a point.
    /// </summary>
    /// <param name="latLon"></param>
    /// <returns></returns>
    public static string PointPath(PositionBase latLon)
    {
        // if latLon is of type NullPosition return an empty string
        if (latLon is NullPosition)
        {
            return string.Empty;
        }
        else
        {
            return $"/points/{latLon.Latitude},{latLon.Longitude}";
        }
    }

    public static string GridpointsForecastPath(PositionBase latLon)
    {
        // if latLon is of type NullPosition return an empty string
        if (latLon is NullPosition)
        {
            return string.Empty;
        }
        else
        {
            return $"/gridpoints/{latLon.Latitude},{latLon.Longitude}/forecast";
        }
    }

    public static async Task<IResult> GetForecastsAsync(float lat, float lon, WebApplication app)
    {
        var position = PositionBase.Create(lat.ToString(), lon.ToString());

        if (position is NullPosition)
        {
            return Results.BadRequest("Invalid latitude (lat) or langitude (lon) values.");
        }

        using IServiceScope scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();

        logger.LogInformation("Fetching Points metadata for (lat, lon): ({position}).", position);

        var pointsHttpClient = httpClientFactory.CreateClient("NwsApi");
        var request = new HttpRequestMessage(
            HttpMethod.Get, PointPath((Position)position)
            );
        var pointsResponse = await pointsHttpClient.SendAsync(request);

        if (pointsResponse.IsSuccessStatusCode)
        {
            string pointsResponseJson = await pointsResponse.Content.ReadAsStringAsync();
            logger.LogInformation("Received Points metadata: {pointjson}", pointsResponseJson);
            PointsResponse? points = null;

            try
            {
                points = JsonSerializer.Deserialize<PointsResponse>(pointsResponseJson, jsonOptions);
            }
            catch (JsonException jex)
            {
                logger.LogError("JSON Deserialization error: {message}, {jsonpath}, {stacktrace}", jex.Message, jex.Path, jex.StackTrace);
                logger.LogError("Unable to process data from the NWS NOAA API. Try again later.");
            }

            if (points is null)
            {
                logger.LogWarning("Failed to deserialize PointsResponse for location ({location}).", position.ToString());
                return Results.Problem("NWS API returned a response but it was empty. Try again later or try using a different location.");
            }

            if (string.IsNullOrWhiteSpace(points.Forecast))
            {
                logger.LogWarning("PointsResponse for location ({location}) has no forecast URL, returning.", position.ToString());
                return Results.Problem("NWS API returned a response but it was empty. Try again later or try using a different location.");
            }

            logger.LogInformation("Deserialized PointsResponse: {points}", points);

            // call the full nwsapi forecast url
            var forecastHttpClient = httpClientFactory.CreateClient("NwsElementUrl");
            request = new HttpRequestMessage(
                HttpMethod.Get, points.Forecast
                );
            var forecastResponse = await forecastHttpClient.SendAsync(request);
            string? forecastJson;

            if (forecastResponse.IsSuccessStatusCode)
            {
                forecastJson = await forecastResponse.Content.ReadAsStringAsync();
                logger.LogInformation("Fetch returned Forecast json: {forecastjson}", forecastJson);
            }
            else
            {
                logger.LogWarning("Failed to fetch Forecast for location ({location}).", position.ToString());
                return Results.Problem("NWS API did not return a result. Try again later.");
            }

            ForecastResponse? forecastInstance = JsonSerializer.Deserialize<ForecastResponse>(forecastJson, jsonOptions);

            if (forecastInstance is not null)
            {
                string forecastCount = forecastInstance.Periods.Count.ToString();
                string forecasts = forecastInstance.Periods.Count == 1
                    ? "forecast"
                    : "forecasts";
                logger.LogInformation("Deserialized {forecastcount} {forecasts}", forecastCount, forecasts);
            }
            else
            {
                logger.LogWarning("Failed to deserialize any forecasts for location ({location}).", position.ToString());
                return Results.Problem("Unable to deserialize response from the NWS API. Try again later.");
            }

            return Results.Ok(forecastInstance);
        }

        logger.LogError("Failed to get forecast data for location ({location}).", position.ToString());
        return Results.Problem("Failed to get forecast data from the NWS API. Try again later.");
    }
}
