
using MobWx.Lib.Models.Geocoding;

namespace MobWx.API.Common
{
    public interface IOpenStreetMapsAbstraction
    {
        Task<string> GetGeoJsonDataAsync(Location location);
    }
}