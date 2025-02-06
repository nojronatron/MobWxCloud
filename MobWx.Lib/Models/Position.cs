using MobWx.Lib.Models.Base;

namespace MobWx.Lib.Models;

public class Position : PositionBase
{
    public Position(string latitude, string longitude): base(latitude, longitude)
    {
    }
}
