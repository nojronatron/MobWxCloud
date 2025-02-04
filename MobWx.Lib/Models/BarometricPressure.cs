namespace MobWx.Lib.Models;

/// <summary>
/// NWS API returns Barometric Pressure as unit Pascals (Pa) as a Double value type
/// although NWS API often returns the value as an integer, boxing is acceptable.
/// See https://codes.wmo.int/common/unit/_Pa
/// </summary>
public class BarometricPressure : Measurement
{
    public override string ToString()
    {
        return Value is null ? string.Empty : Value.Value.ToString();
    }
}
