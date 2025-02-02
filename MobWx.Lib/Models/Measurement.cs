namespace MobWx.Lib.Models;

using System.Text.Json.Serialization;

public class Measurement
{
    [JsonPropertyName("unitCode")]
    public string UnitCode { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public double? Value { get; set; }

    [JsonPropertyName("qualityControl")]
    public string QualityControl { get; set; } = string.Empty;

    public static Measurement MinValue = new() { Value = double.MinValue };

    public static Measurement ZeroValue = new() { Value = 0 };

    public bool IsNull => Value is null;

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
    /// Convert the value to a string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Value is not null ? $"{Value}" : string.Empty;
    }
}
