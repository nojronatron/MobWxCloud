using MobWx.Lib.Models.Base;
using System.Text.Json.Serialization;

namespace MobWx.Lib.Models;

/// <summary>
/// Use this class to represent an NWS API measurement instance with a 32-bit Integer value type.
/// </summary>
public class MeasurementInt : MeasurementBase
{
    [JsonPropertyName("value")]
    public int? Value { get; set; }

    public static MeasurementInt ZeroValue => new() { Value = 0 };

    public static MeasurementInt MinValue => new() { Value = int.MinValue };

    public bool HasNullValue => Value is null;

    /// <summary>
    /// Convert the value to an integer after converting to a value with LESS precision.
    /// See .NET 9 documentation "Midpoint values and rounding conventions" for more information.
    /// https://learn.microsoft.com/en-us/dotnet/api/system.math.round?view=net-9.0#midpoint-values-and-rounding-conventions
    /// </summary>
    /// <returns></returns>
    public int ToInt()
    {
        return (int)Math.Round((double)Value!, 0);
    }

    /// <summary>
    /// Convert a possibly Null value to an integer after converting to a value with LESS precision.
    /// If Value is Null, return Null.
    /// </summary>
    /// <returns></returns>
    public int? ToNullableInt()
    {
        return Value is not null ? (int)Math.Round((double)Value, 0) : null;
    }

    /// <summary>
    /// Convert the value to a double.
    /// </summary>
    /// <returns></returns>
    public double ToDouble()
    {
        return (double)Math.Round((double)Value!, 2);
    }

    /// <summary>
    /// Convert the value to a nullable double.
    /// </summary>
    /// <returns></returns>
    public double? ToNullableDouble()
    {
        return Value is not null ? (double)Math.Round((double)Value, 2) : null;
    }

    /// <summary>
    /// Convert the value to a string. If value is null, returns "null".
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Value is not null ? $"{Value}" : "null";
    }
}
