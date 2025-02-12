using MobWx.Lib.Models;
using MobWx.Lib.Models.Base;
using System.Diagnostics.CodeAnalysis;

namespace MobWx.API.Models;

public class Coordinate : IParsable<Coordinate>
{
    public Latitude? Lat { get; set; }
    public Longitude? Lon { get; set; }

    public Coordinate(double lat, double lon)
    {
        Lat = new Latitude { Value = lat };
        Lon = new Longitude { Value = lon };
    }

    public static Coordinate Parse(string s, IFormatProvider? provider)
    {
        string[] parts = s.Split(',');
        if (parts.Length != 2)
        {
            throw new FormatException("Coordinate string must contain two parts separated by a comma.");
        }
        else
        {
            return new Coordinate(double.Parse(parts[0]), double.Parse(parts[1]));
        }
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Coordinate result)
    {
        try
        {
            result = Parse(s, null);
            return true;
        }
        catch (FormatException)
        {
            result = null;
            return false;
        }
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

        return new Position(Lat.GetValue().ToString(), Lon.GetValue().ToString());
    }
}

public class Latitude
{
    public const double MaxValue = 90.0;
    public double Value { get; set; }

    public double GetValue()
    {
        int sign = Value < 0 ? -1 : 1;
        double currVal = Math.Abs(Value);

        while (currVal > MaxValue)
        {
            currVal -= MaxValue;
        }

        return sign * currVal;
    }
}

public class Longitude
{
    public const double MaxValue = 180.0;
    public double Value { get; set; }

    public double GetValue()
    {
        int sign = Value < 0 ? -1 : 1;
        double currVal = Math.Abs(Value);

        while (currVal > MaxValue)
        {
            currVal -= MaxValue;
        }

        return sign * currVal;
    }
}
