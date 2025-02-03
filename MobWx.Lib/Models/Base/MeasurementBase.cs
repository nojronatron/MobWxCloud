using System.Text.Json.Serialization;

namespace MobWx.Lib.Models.Base
{
    public class MeasurementBase
    {
        [JsonPropertyName("unitCode")]
        public string UnitCode { get; set; } = string.Empty;

        [JsonPropertyName("qualityControl")]
        public string QualityControl { get; set; } = string.Empty;
    }
}
