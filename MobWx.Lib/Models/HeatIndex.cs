namespace MobWx.Lib.Models;

public class HeatIndex : Measurement
{
    public override string ToString()
    {
        return Value is null ? string.Empty : Value.Value.ToString("F2");
    }
}
