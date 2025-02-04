namespace MobWx.Lib.Models;

/// <summary>
/// NWS API returns Barometric Pressure as unit Pascals (Pa) with a 32-bit Integer value type.
/// See https://codes.wmo.int/common/unit/_Pa
/// </summary>
public class BarometricPressure : MeasurementInt
{
    public override string ToString()
    {
        return Value is null ? string.Empty : Value.Value.ToString();
    }
}
