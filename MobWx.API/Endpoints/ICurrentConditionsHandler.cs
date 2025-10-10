using MobWx.Lib.Models;

namespace MobWx.API.Endpoints
{
    public interface ICurrentConditionsHandler
    {
        Task<IResult> GetCurrentConditionsAsync(Position position);
    }
}