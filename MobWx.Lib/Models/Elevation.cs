namespace MobWx.Lib.Models;

/// <summary>
/// NWS API returns Elevation as unit Meters (m) (as a Double value type???)
/// </summary>
public class Elevation : QuantitativeValueDouble
{
   public override string ToString()
    {
        return Value is null ? string.Empty : Value.Value.ToString();
    }
}
