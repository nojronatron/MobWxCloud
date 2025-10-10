using System.Text.Json.Serialization;

namespace MobWx.Lib.Models.Geocoding;

/// <summary>
/// Nominatim Feature GeoJSON definition. See https://nominatim.org/release-docs/develop/api/Search/
/// </summary>
public class NominatimFeature
{
    [JsonPropertyName("type")]
    public string? FeatureType { get; set; }

    [JsonPropertyName("properties")]
    public NominatimFeatureProperties? FeatureProperties { get; set; }

    // [ lon, lat, lon, lat, ..., ... ]
    [JsonPropertyName("bbox")]
    public List<double> BBox { get; set; } = [];

    [JsonPropertyName("geometry")]
    public NominatimGeometry? Geometry { get; set; }
}
