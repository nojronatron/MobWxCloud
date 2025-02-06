using MobWx.Lib.Models.Base;

namespace MobWx.Lib.Models;

public class NullPosition : PositionBase
{
    public NullPosition() : base(string.Empty, string.Empty)
    {
    }

    public override string ToString()
    {
        return string.Empty;
    }
}
