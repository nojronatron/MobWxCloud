using MobWx.Lib.ForecastModels;
using MobWx.Lib.Models;
using MobWx.Lib.PointModels;

namespace MobWx.API.Common
{
    public interface IJsonHandler
    {
        IEnumerable<string?> GetObservationStationsList(string objStationsUrlListJson);
        string GetObservationStationsUrl(string points);
        ForecastResponse? TryDeserializeForecastResponseAsync(string forecastResponseJson);
        Observation? TryDeserializeObservation(string jsonString);
        PointsResponse? TryDeserializePointsResponse(string jsonString);
    }
}