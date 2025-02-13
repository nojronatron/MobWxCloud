
namespace MobWx.API.Endpoints
{
    public interface ILocationHandler
    {
        Task<IResult> GetGeoLocationAsync(string city, string state);
        bool TryParseLocation(string city, string state, out string cleanCity, out string cleanState);
    }
}