namespace MobWx.Lib.Models;

using System.Text.Json.Serialization;

public class CloudLayer
{
    [JsonPropertyName("base")]
    public Measurement? Base { get; set; } = Measurement.ZeroValue;

    [JsonPropertyName("amount")]
    public Amount Amount { get; set; }
}
