using System.Text.Json.Serialization;

namespace MobWx.Lib.Models
{
    public class QuantitativeValueDouble : QuantitativeValue
    {
        [JsonPropertyName("maxValue")]
        public double? MaxValue { get; set; }

        [JsonPropertyName("minValue")]
        public double? MinValue { get; set; }
    }
}
