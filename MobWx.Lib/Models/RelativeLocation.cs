using System.Text.Json.Serialization;

namespace MobWx.Lib.Models;

public class RelativeLocation
{
    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("distance")]
    public QuantitativeValue? Distance { get; set; }

    [JsonPropertyName("bearing")]
    public QuantitativeValue? Bearing { get; set; }

    public string? DistanceKm => Distance is not null && Distance.Value is not null ? $"{Math.Round(Distance.Value.Value / 100, 1)}" : null;
    public string? DistanceMi => Distance is not null && Distance.Value is not null ? $"{Math.Round(Distance.Value.Value / 160.9344, 1)}" : null;
    public string? BearingDegrees => Bearing is not null && Bearing.Value is not null ? $"{Bearing.Value.Value}" : null;
}
