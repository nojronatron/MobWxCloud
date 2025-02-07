using MobWx.Lib.Models;
using MobWx.Lib.Models.Base;

namespace MobWx.API.Common
{
    public interface INwsEndpointAbstraction
    {
        Task<string> GetObservationStationsAsync(string url);
        //IEnumerable<string?> GetObservationStationsList(string objStationsUrlListJson);
        //string GetObservationStationsUrl(string points);
        Task<string> GetPointDataAsync(PositionBase position);
        //Observation? TryDeserializeObservation(string jsonString);
        Task<string> TryGetObservationAsync(string observationStationId);
    }
}