using MobWx.Lib.Models.Base;

namespace MobWx.API.Endpoints;

public interface IAlertsHandler
{
    Task<IResult> GetActiveAlertsAsync(PositionBase position);
}
