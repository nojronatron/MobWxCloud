namespace MobWx.Lib.Models;

public class WindGust : Measurement
{
    public override string ToString()
    {
        return Value?.ToString("F1") ?? string.Empty;
    }
}
