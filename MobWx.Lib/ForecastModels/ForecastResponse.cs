using MobWx.Lib.Models;
using System.Text.Json.Serialization;

namespace MobWx.Lib.ForecastModels
{
    public class ForecastResponse
    {
        [JsonPropertyName("generatedAt")]
        public DateTime? GeneratedAt { get; set; }

        [JsonPropertyName("updateTime")]
        public DateTime? UpdateTime { get; set; }

        [JsonPropertyName("elevation")]
        public QuantitativeValue? Elevation { get; set; }

        [JsonPropertyName("periods")]
        public List<ForecastPeriod> Periods { get; set; } = [];
    }
}
