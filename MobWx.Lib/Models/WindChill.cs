namespace MobWx.Lib.Models;

public class WindChill : Measurement
{
    public override string ToString()
    {
        return Value is null ? string.Empty : Value.Value.ToString("F1");
    }
}
