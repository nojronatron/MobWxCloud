using System.Text.Json.Serialization;

namespace MobWx.Lib.Models.Geocoding;

public class Geometry
{
    [JsonPropertyName("geometry")]
    public string? GeometryType { get; set; }

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
    /// Get the position of the geometry
    /// </summary>
    /// <returns></returns>
    public Position GetPosition()
    {
        return new Position(CoordinateDoubles[1].ToString(), CoordinateDoubles[0].ToString());
    }
}
