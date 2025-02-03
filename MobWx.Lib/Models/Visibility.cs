namespace MobWx.Lib.Models;

public class Visibility : MeasurementInt
{
    public override string ToString()
    {
        return Value is null ? string.Empty : Value.Value.ToString();
    }
}
