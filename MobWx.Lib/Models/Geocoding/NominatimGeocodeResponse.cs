using System.Text.Json.Serialization;

namespace MobWx.Lib.Models.Geocoding;

/// <summary>
/// Base GeoCode Response GeoJSON definition. See https://nominatim.org/release-docs/develop/api/Search/
/// </summary>
public class NominatimGeocodeResponse
{
    [JsonPropertyName("type")]
    public string? ResponseType { get; set; } // "type": "FeatureCollection"

    [JsonPropertyName("license")]
    public string License { get; set; } = "Data © OpenStreetMap contributors, ODbL 1.0. https://osm.org/copyright";

    [JsonPropertyName("features")]
    public List<NominatimFeature> Features { get; set; } = [];
}
