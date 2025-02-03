namespace MobWx.Lib.Models;

public class WindDirection : MeasurementInt
{
    public override string ToString()
    {
        return Value is null ? string.Empty : Value.Value.ToString();
    }
}
