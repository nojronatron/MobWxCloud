using MobWx.API.Common;
using MobWx.Lib.Models;
using MobWx.Lib.Models.Base;
using MobWx.Lib.NwsAlertModels;

namespace MobWx.API.Endpoints;

public class AlertsHandler : IAlertsHandler
{
    private readonly INwsEndpointAbstraction _nwsEndpoint;
    private readonly ILogger<AlertsHandler> _logger;
    private readonly IJsonHandler _jsonHandler;

    public AlertsHandler(
        INwsEndpointAbstraction nwsEndpoint,
        ILogger<AlertsHandler> logger, 
        IJsonHandler jsonHandler)
    {
        _nwsEndpoint = nwsEndpoint;
        _logger = logger;
        _jsonHandler = jsonHandler;
    }

    /// <summary>
    /// Orchestrates fetching active alerts from the NWS API for a given position.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="httpRequest"></param>
    /// <param name="position"></param>
    /// <returns>Awaitable IResult with HTTP Status Code and Alert data or empty.</returns>
    public async Task<IResult> GetActiveAlertsAsync(PositionBase position)
    {
        if (position is NullPosition)
        {
            return Results.BadRequest("Invalid latitude (lat) or langitude (lon) values.");
        }

        string? activeAlertJson = await _nwsEndpoint.GetNwsAlertsAsync(position);

        if (string.IsNullOrWhiteSpace(activeAlertJson))
        {
            _logger.LogWarning("Failed to get active alerts for location ({location}).", position.ToString());
            return Results.NotFound();
        }

        ActiveAlerts? activeAlerts = _jsonHandler.TryDeserializeActiveAlerts(activeAlertJson);

        if (activeAlerts is not null)
        {
            string alertCount = activeAlerts.Count.ToString();
            string alerts = activeAlerts.Count == 1
                ? "alert"
                : "alerts";
            _logger.LogInformation("Deserialized {alertCount} active {alerts}: {activeAlerts}", alertCount, alerts, activeAlerts);
            return Results.Ok(activeAlerts);
        }
        else
        {
            _logger.LogWarning("Failed to deserialize any active alerts for location ({location}).", position.ToString());
        }

        return Results.Ok();
    }
}
