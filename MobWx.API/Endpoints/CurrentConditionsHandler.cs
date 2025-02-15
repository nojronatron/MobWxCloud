using MobWx.API.Common;
using MobWx.Lib.Models.Base;
using MobWx.Lib.Models;
using MobWx.Lib.Models.Nws;

namespace MobWx.API.Endpoints;

public class CurrentConditionsHandler : ICurrentConditionsHandler
{
    private readonly IJsonHandler _jsonHandler;
    private readonly ILogger<CurrentConditionsHandler> _logger;
    private readonly INwsEndpointAbstraction _nwsEndpointAbstraction;

    public CurrentConditionsHandler(INwsEndpointAbstraction nwsEndpointAbstraction, IJsonHandler jsonHandler, ILogger<CurrentConditionsHandler> logger)
    {
        _nwsEndpointAbstraction = nwsEndpointAbstraction;
        _jsonHandler = jsonHandler;
        _logger = logger;
    }

    /// <summary>
    /// Orchcestrates execution steps necessary to get Current Conditions from NWS API.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public async Task<IResult> GetCurrentConditionsAsync(PositionBase position)
    {
        if (position is NullPosition)
        {
            return Results.NotFound("Position is invalid. Try again with a valid location.");
        }

        var pointData = await _nwsEndpointAbstraction.GetPointDataAsync(position);

        if (string.IsNullOrEmpty(pointData))
        {
            return Results.NotFound("Points response was empty for position. Try again later or try a different location.");
        }

        string stationUrl = _jsonHandler.GetObservationStationsUrl(pointData);

        if (string.IsNullOrEmpty(stationUrl))
        {
            return Results.NotFound("List of observation stations was empty. Try again later or try a different location.");
        }

        string nearestObsStationsResponse = await _nwsEndpointAbstraction.GetObservationStationsAsync(stationUrl);

        if (string.IsNullOrEmpty(nearestObsStationsResponse))
        {
            return Results.NotFound("Observation station did not return data. Try again later or try a different location.");
        }

        IEnumerable<string?> observationStations = _jsonHandler.GetObservationStationsList(nearestObsStationsResponse);

        foreach (var obsStnUrl in observationStations)
        {
            if (string.IsNullOrWhiteSpace(obsStnUrl))
            {
                _logger.LogWarning("ObsStnUrl was null, moving to next item.");
                continue;
            }

            string observationJson = await _nwsEndpointAbstraction.TryGetObservationAsync(obsStnUrl);
            _logger.LogInformation("Response Content JSON string is: {observationjson}", observationJson);

            var deserializedObservation = _jsonHandler.TryDeserializeObservation(observationJson);

            if (deserializedObservation is not null)
            {
                var currentObservation = CurrentObservation.Create(deserializedObservation);
                _logger.LogInformation("Returning current observation content in HTTP Response.");
                return Results.Ok(currentObservation);
            }

            _logger.LogWarning("Observation object was empty. Will try next (if there is one).");
        }

        return Results.Problem("Unable to get an observation. Try again later or try a different location.");
    }
}
