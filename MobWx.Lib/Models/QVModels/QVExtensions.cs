namespace MobWx.Lib.Models.QVModels;

public static class QVExtensions
{
    /// <summary>
    /// Convert the value to an integer after converting to a value with LESS precision.
    /// See .NET 9 documentation "Midpoint values and rounding conventions" for more information.
    /// https://learn.microsoft.com/en-us/dotnet/api/system.math.round?view=net-9.0#midpoint-values-and-rounding-conventions
    /// </summary>
    /// <returns></returns>
    public static int ToInt(this QuantitativeValue qv)
    {
        return (int)Math.Round((double)qv.Value!, 0);
    }

    /// <summary>
    /// Convert a possibly Null value to an integer after converting to a value with LESS precision.
    /// If Value is Null, return Null.
    /// </summary>
    /// <returns></returns>
    public static int? ToNullableInt(this QuantitativeValue qv)
    {
        return qv.Value is not null ? (int)Math.Round((double)qv.Value, 0) : null;
    }

    /// <summary>
    /// Convert the value to a double.
    /// </summary>
    /// <returns></returns>
    public static double ToDouble(this QuantitativeValue qv)
    {
        return (double)Math.Round((double)qv.Value!, 2);
    }

    /// <summary>
    /// Convert the value to a nullable double.
    /// </summary>
    /// <returns></returns>
    public static double? ToNullableDouble(this QuantitativeValue qv)
    {
        return qv.Value is not null ? (double)Math.Round((double)qv.Value, 2) : null;
    }

    /// <summary>
    /// Convert the value to a string. If value is null, returns "null".
    /// </summary>
    /// <returns></returns>
    public static string ToFormatted(this QuantitativeValue qv, string format)
    {
        return qv.Value?.ToString(format) ?? string.Empty;
    }
}
