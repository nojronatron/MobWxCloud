using MobWx.Lib.Models.Base;

namespace MobWx.Lib.Models;

public class Coordinate : IEquatable<Coordinate>
{
    public double? Lat { get; set; }
    public double? Lon { get; set; }

    public Coordinate(double lat, double lon)
    {
        Lat = lat;
        Lon = lon;
    }

    /// <summary>
    /// Converts the Coordinate object to a Position object.
    /// </summary>
    /// <returns></returns>
    public PositionBase ToPosition()
    {
        if (Lat is null || Lon is null)
        {
            return new NullPosition();
        }

        return new Position(Lat.ToString(), Lon.ToString());
    }

    /// <summary>
    /// Determines if the current Coordinate object has null values.
    /// </summary>
    /// <returns></returns>
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
    /// Determines if the current Coordinate object is equal to another Coordinate object.
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
