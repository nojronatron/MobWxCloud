namespace MobWx.Lib.Models;

public class MinTemperatureLast24Hours : Measurement
{
    public override string ToString()
    {
        return Value is null ? string.Empty : Value.Value.ToString("F2");
    }
}
