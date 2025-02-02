using MobWx.Lib.Models;

namespace MobWx.Lib.Common
{
    public interface INwsApiAbstraction
    {
        Task<Observation> GetCurrentConditionsAsync(PositionBase position);
        Task<string> GetObservationStationsAsync(string url);
        IEnumerable<string?> GetObservationStationsList(string objStationsUrlListJson);
        string GetObservationStationsUrl(string points);
        Task<string> GetPointDataAsync(PositionBase position);
        Task<Observation> TryGetObservationAsync(IEnumerable<string?> observationStations);
    }
}