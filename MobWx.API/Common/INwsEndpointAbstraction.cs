using MobWx.Lib.Models;
using MobWx.Lib.PointModels;

namespace MobWx.API.Common
{
    public interface INwsEndpointAbstraction
    {
        Task<string> GetNwsAlertsAsync(Position position);
        Task<string> GetNwsForecastsAsync(PointsResponse points);
        Task<string> GetObservationStationsAsync(string url);
        Task<string> GetPointDataAsync(Position position);
        Task<string> TryGetObservationAsync(string observationStationId);
    }
}