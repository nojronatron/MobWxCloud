namespace MobWx.Lib.Models;

public class Elevation : Measurement
{
   public override string ToString()
    {
        return Value is null ? string.Empty : Value.Value.ToString();
    }
}
