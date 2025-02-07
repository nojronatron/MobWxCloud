using MobWx.Lib.Models;

namespace MobWx.API.Common
{
    public interface IJsonHandler
    {
        IEnumerable<string?> GetObservationStationsList(string objStationsUrlListJson);
        string GetObservationStationsUrl(string points);
        Observation? TryDeserializeObservation(string jsonString);
    }
}