using System.Text.Json.Serialization;

namespace MobWx.Lib.Models.Geocoding;

/// <summary>
/// Nominatim Geometry GeoJSON  definition. See https://nominatim.org/release-docs/develop/api/Search/
/// </summary>
public class NominatimGeometry
{
    [JsonPropertyName("type")]
    public string? GeometryType { get; set; } // e.g. "Point"

    [JsonPropertyName("coordinates")]
    public List<double> CoordinateDoubles { get; set; } = [];

    /// <summary>
    /// Get the location of the geometry
    /// </summary>
    /// <returns></returns>
    public Location GetLocation()
    {
        return new Location(CoordinateDoubles[1], CoordinateDoubles[0]);
    }

    /// <summary>
    /// Returns a string representation of the geometry like "Point(-123.456789, 12.345678)"
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"Point({CoordinateDoubles[1]}, {CoordinateDoubles[0]})";
    }
}
