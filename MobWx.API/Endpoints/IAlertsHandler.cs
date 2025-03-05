using MobWx.Lib.Models;

namespace MobWx.API.Endpoints;

public interface IAlertsHandler
{
    Task<IResult> GetActiveAlertsAsync(Position position);
}
