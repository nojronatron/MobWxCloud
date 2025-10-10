using MobWx.Lib.Models;

namespace MobWx.API.Endpoints;

public interface IForecastsHandler
{
    Task<IResult> GetForecastsAsync(Position position);
}
