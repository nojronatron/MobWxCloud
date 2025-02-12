using MobWx.Lib.Models.Base;

namespace MobWx.API.Endpoints;

public interface IForecastsHandler
{
    Task<IResult> GetForecastsAsync(PositionBase position);
}
