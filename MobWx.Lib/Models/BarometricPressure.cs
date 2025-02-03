namespace MobWx.Lib.Models;

public class BarometricPressure : MeasurementInt
{
    public override string ToString()
    {
        if (string.IsNullOrWhiteSpace(UnitCode))
        {
            UnitCode = ":null";
        }

        return Value is null ? string.Empty : Value.Value.ToString();
    }
}
