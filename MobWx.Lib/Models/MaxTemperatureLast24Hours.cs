namespace MobWx.Lib.Models;

public class MaxTemperatureLast24Hours : Measurement
{
    public override string ToString()
    {
        return Value is null ? string.Empty : Value.Value.ToString("F2");
    }
}
