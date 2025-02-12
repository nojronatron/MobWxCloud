using MobWx.Lib.Models.Base;

namespace MobWx.API.Endpoints
{
    public interface ICurrentConditionsHandler
    {
        Task<IResult> GetCurrentConditionsAsync(PositionBase position);
    }
}