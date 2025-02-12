using System.Text.Json.Serialization;

namespace MobWx.Lib.Models
{
    public class QuantitativeValue : Measurement
    {
        [JsonPropertyName("maxValue")]
        public double? MaxValue { get; set; }

        [JsonPropertyName("minValue")]
        new public double? MinValue { get; set; }

        public override string ToString()
        {
            return Value?.ToString("F1") ?? string.Empty;
        }
    }
}
