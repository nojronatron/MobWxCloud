using System.Text.Json.Serialization;

namespace MobWx.Lib.Models;

public class QuantitativeValue
{
    [JsonPropertyName("value")]
    public double? Value { get; set; }

    [JsonPropertyName("maxValue")]
    public double? MaxValue { get; set; }

    [JsonPropertyName("minValue")]
    public double? MinValue { get; set; }

    [JsonPropertyName("unitCode")]
    public string UnitCode { get; set; } = string.Empty;

    [JsonPropertyName("qualityControl")]
    public string QualityControl { get; set; } = string.Empty;

    /// <summary>
    /// Print Value as a string, or an empty string if Value is null.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Value.HasValue ? Value.Value.ToString() : string.Empty;
    }
}
