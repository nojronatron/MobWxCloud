using MobWx.Lib.Models.Geocoding;

namespace MobWx.Lib.Models;

public class Position
{
    public Coordinate? Coordinate { get; set; }

    public Location? Location { get; set; }

    /// <summary>
    /// Returns true if Coordinate is not null and has non-null values in its Lat and Lon properties.
    /// </summary>
    public bool HasCoordinates => Coordinate is not null && !Coordinate.HasNulls();
    public bool HasLocation => Location is not null && !string.IsNullOrWhiteSpace(Location.CityName) && !string.IsNullOrWhiteSpace(Location.StateAbbreviation);

    /// <summary>
    /// Returns a new Position instance with Coordinate and Location properties set.
    /// </summary>
    /// <param name="coordinate"></param>
    /// <param name="location"></param>
    /// <returns></returns>
    public static Position Create(Coordinate coordinate, Location location)
    {
        return new Position
        {
            Coordinate = coordinate,
            Location = location
        };
    }

    /// <summary>
    /// Returns a new Position instance with only Coordinate property set.
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public static Position Create(Coordinate coordinate)
    {
        return new Position
        {
            Coordinate = coordinate
        };
    }

    /// <summary>
    /// Returns a new Position instance with only Location property set.
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public static Position Create(Location location)
    {
        return new Position
        {
            Location = location
        };
    }
}
