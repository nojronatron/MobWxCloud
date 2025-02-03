namespace MobWx.Lib.Models;

public class MaxTemperatureLast24Hours : Measurement
{
    public override string ToString()
    {
        if (string.IsNullOrWhiteSpace(UnitCode))
        {
            UnitCode = ":null";
        }

        return Value is null ? string.Empty : Value.Value.ToString("F2");
    }
}
