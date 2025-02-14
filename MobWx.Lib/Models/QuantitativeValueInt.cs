using System.Text.Json.Serialization;

namespace MobWx.Lib.Models
{
    public class QuantitativeValueInt : QuantitativeValue
    {
        [JsonPropertyName("value")]
        new public int? Value { get; set; }

        [JsonPropertyName("maxValue")]
        public int? MaxValue { get; set; }

        [JsonPropertyName("minValue")]
        public int? MinValue { get; set; }
    }
}
