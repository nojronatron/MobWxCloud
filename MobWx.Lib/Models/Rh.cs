namespace MobWx.Lib.Models;

public class Rh : Measurement
{
    public override string ToString()
    {
        return Value is not null ? Value.Value.ToString() : string.Empty;
    }
}
