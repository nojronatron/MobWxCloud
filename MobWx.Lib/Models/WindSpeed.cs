namespace MobWx.Lib.Models;

public class WindSpeed : Measurement
{
    public override string ToString()
    {
        return Value?.ToString("F2") ?? string.Empty;
    }
}
