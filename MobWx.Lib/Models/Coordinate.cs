namespace MobWx.Lib.Models;

public class Coordinate : IEquatable<Coordinate>
{
    public double? Lat { get; set; }
    public double? Lon { get; set; }

    public QuantitativeValue? Elevation { get; set; }

    /// <summary>
    /// Create a new concrete Coordinate instance.
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    /// <returns></returns>
    public static Coordinate Create(double lat, double lon)
    {
        return new Coordinate
        {
            Lat = lat,
            Lon = lon
        };
    }

    /// <summary>
    /// Create a new concrete Coordinate instance.
    /// </summary>
    /// <param name="lat"></param>
    /// <param name="lon"></param>
    /// <returns></returns>
    public static Coordinate Create(string lat, string lon)
    {
        double.TryParse(lat, out double latValue);
        double.TryParse(lon, out double lonValue);

        return new Coordinate
        {
            Lat = latValue,
            Lon = lonValue
        };
    }

    /// <summary>
    /// Determines if the current Coordinate object has null values.
    /// Does not consider Elevation.
    /// </summary>
    /// <returns>True if Lat and/or Lon are null, otherwise false.</returns>
    public bool HasNulls()
    {
        return Lat is null || Lon is null;
    }

    /// <summary>
    /// Returns a version of num truncated to the specified number of decimal places.
    /// </summary>
    /// <param name="num"></param>
    /// <param name="places"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static double LimitDecimalPlaces(double num, int places)
    {
        if (places < 0 || places > 7)
        {
            throw new ArgumentOutOfRangeException(nameof(places), "The number of decimal places must be between 0 and 7.");
        }

        double multiplier = Math.Pow(10, places);
        return Math.Round(num * multiplier) / multiplier;
    }

    /// <summary>
    /// Determines if the current Coordinate object is equal to another Coordinate
    /// object based on their Lat and Lon values, limited to 2 decimal places 
    /// (just over 1 km N/S or E/W at the equator).
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Coordinate? other)
    {
        if (HasNulls() || other is null)
        {
            return false;
        }

        if (other.HasNulls())
        {
            return false;
        }

        return
            LimitDecimalPlaces(Lat!.Value, 2) == LimitDecimalPlaces(other.Lat!.Value, 2) &&
            LimitDecimalPlaces(Lon!.Value, 2) == LimitDecimalPlaces(other.Lon!.Value, 2);
    }

    /// <summary>
    /// Determines if the current object is a Coordinate Type and is equal to another Coordinate object.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return Equals(obj as Coordinate);
    }

    /// <summary>
    /// Returns the hash code for the current Coordinate object.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Lat, Lon);
    }
}
