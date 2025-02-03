namespace MobWx.Lib.Models;

public class Dewpoint : Measurement
{
    public override string ToString()
    {
        return Value is null ? string.Empty : Value.Value.ToString("F1");
    }
}
