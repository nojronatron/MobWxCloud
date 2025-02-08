using MobWx.Lib.Models;
using MobWx.Lib.Models.Base;
using MobWx.Lib.PointModels;

namespace MobWx.API.Common
{
    public interface INwsEndpointAbstraction
    {
        Task<string> GetNwsAlertsAsync(PositionBase position);
        Task<string> GetNwsForecastsAsync(PointsResponse points);
        Task<string> GetNwsPointsAsync(Position position);
        Task<string> GetObservationStationsAsync(string url);
        Task<string> GetPointDataAsync(PositionBase position);
        Task<string> TryGetObservationAsync(string observationStationId);
    }
}