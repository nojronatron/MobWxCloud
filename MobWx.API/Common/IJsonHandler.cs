using MobWx.Lib.ForecastModels;
using MobWx.Lib.Models;
using MobWx.Lib.Models.Geocoding;
using MobWx.Lib.NwsAlertModels;
using MobWx.Lib.PointModels;

namespace MobWx.API.Common
{
    public interface IJsonHandler
    {
        IEnumerable<string?> GetObservationStationsList(string objStationsUrlListJson);
        string GetObservationStationsUrl(string points);
        ActiveAlerts? TryDeserializeActiveAlerts(string activeAlertJson);
        ForecastResponse? TryDeserializeForecastResponse(string forecastResponseJson);
        Observation? TryDeserializeObservation(string jsonString);
        PointsResponse? TryDeserializePointsResponse(string jsonString);
        GeocodeResponse? TryDeserializeGeocodeResponse(string geoJsonData);
    }
}