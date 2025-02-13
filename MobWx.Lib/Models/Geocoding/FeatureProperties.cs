using System.Text.Json.Serialization;

namespace MobWx.Lib.Models.Geocoding;

public class FeatureProperties
{
    [JsonPropertyName("place_id")]
    public int? PlaceId { get; set; }

    [JsonPropertyName("osm_type")]
    public string? OsmType { get; set; }

    [JsonPropertyName("osm_id")]
    public int? OsmId { get; set; }

    [JsonPropertyName("place_rank")]
    public int? PlaceRank { get; set; }

    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("addresstype")]
    public string? AddressType { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("display_name")]
    public string? DisplayName { get; set; }
}
