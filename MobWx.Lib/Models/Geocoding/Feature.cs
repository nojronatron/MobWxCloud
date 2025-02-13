using System.Text.Json.Serialization;

namespace MobWx.Lib.Models.Geocoding;

public class Feature
{
    [JsonPropertyName("type")]
    public string? FeatureType { get; set; }

    [JsonPropertyName("properties")]
    public FeatureProperties? FeatureProperties { get; set; }

    // [ lon, lat, lon, lat, ..., ... ]
    [JsonPropertyName("bbox")]
    public List<double> BBox { get; set; } = [];

    [JsonPropertyName("geometry")]
    public Geometry? Geometry { get; set; }
}
