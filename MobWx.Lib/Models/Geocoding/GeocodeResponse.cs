using System.Text.Json.Serialization;

namespace MobWx.Lib.Models.Geocoding;

public class GeocodeResponse
{
    [JsonPropertyName("type")]
    public string? ResponseType { get; set; }

    [JsonPropertyName("license")]
    public string? License { get; set; }

    [JsonPropertyName("features")]
    public List<Feature> Features { get; set; } = [];
}
