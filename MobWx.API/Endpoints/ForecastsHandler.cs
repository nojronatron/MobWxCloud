using MobWx.API.Common;
using MobWx.Lib.ForecastModels;
using MobWx.Lib.Models;
using MobWx.Lib.Models.Base;
using MobWx.Lib.PointModels;

namespace MobWx.API.Endpoints;

public class ForecastsHandler : IForecastsHandler
{
    private readonly ILogger<ForecastsHandler> _logger;
    private readonly IJsonHandler _jsonHandler;
    private readonly INwsEndpointAbstraction _nwsEndpointAbstraction;

    public ForecastsHandler(
        ILogger<ForecastsHandler> logger,
        IJsonHandler jsonHandler,
        INwsEndpointAbstraction nwsEndpointAbstraction
        )
    {
        _logger = logger;
        _jsonHandler = jsonHandler;
        _nwsEndpointAbstraction = nwsEndpointAbstraction;
    }

    public async Task<IResult> GetForecastsAsync(PositionBase position)
    {
        if (position is not NullPosition)
        {
            return Results.BadRequest("Invalid latitude (lat) or langitude (lon) values.");
        }

        string pointsResponseJson = await _nwsEndpointAbstraction.GetNwsPointsAsync((Position)position);

        if (string.IsNullOrEmpty(pointsResponseJson))
        {
            _logger.LogWarning("Failed to get Points metadata for location ({location}).", position.ToString());
            return Results.Problem("Failed to get Points metadata from the NWS API. Try again later.");
        }

        PointsResponse? points = _jsonHandler.TryDeserializePointsResponse(pointsResponseJson);

        if (points is null)
        {
            _logger.LogWarning("Failed to deserialize PointsResponse for location ({location}).", position.ToString());
            return Results.Problem("Failed to deserialize Points metadata from the NWS API. Try again later.");
        }

        if (string.IsNullOrWhiteSpace(points.Forecast))
        {
            _logger.LogWarning("PointsResponse for location ({location}) has no forecast URL, returning.", position.ToString());
            return Results.Problem("NWS API returned a response but the forecast data was empty. Try again later or try using a different location.");
        }

        // call the full nwsapi forecast url
        string? forecastJson = await _nwsEndpointAbstraction.GetNwsForecastsAsync(points);

        if (string.IsNullOrWhiteSpace(forecastJson))
        {
            _logger.LogWarning("Failed to get forecast data for location ({location}).", position.ToString());
            return Results.Problem("Failed to get forecast data from the NWS API. Try again later.");
        }

        ForecastResponse? forecastInstance = _jsonHandler.TryDeserializeForecastResponseAsync(forecastJson);

        if (forecastInstance is not null)
        {
            string forecastCount = forecastInstance.Periods.Count.ToString();
            string forecasts = forecastInstance.Periods.Count == 1
                ? "forecast"
                : "forecasts";
            _logger.LogInformation("Deserialized {forecastcount} {forecasts}", forecastCount, forecasts);
        }
        else
        {
            _logger.LogWarning("Failed to deserialize any forecasts for location ({location}).", position.ToString());
            return Results.Problem("Unable to deserialize response from the NWS API. Try again later.");
        }

        _logger.LogError("Failed to get forecast data for location ({location}).", position.ToString());
        return Results.Problem("Failed to get forecast data from the NWS API. Try again later.");
    }
}
