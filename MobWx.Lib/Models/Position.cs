using MobWx.Lib.Models.Base;

namespace MobWx.Lib.Models;

public class Position : PositionBase
{
    private string _latitude;
    public string _longitude;
    public string _altitude; // optional

    /// <summary>
    /// Returns true if both latitude and longitude are null or whitespace.
    /// </summary>
    public bool IsEmpty
    {
        get => string.IsNullOrWhiteSpace(_latitude)
            && string.IsNullOrWhiteSpace(_longitude);
    }

    private Position()
    {
        _latitude = string.Empty;
        _longitude = string.Empty;
        _altitude = string.Empty;
    }

    public override string GetLatitude()
    {
        return _latitude;
    }

    public override string GetLongitude()
    {
        return _longitude;
    }

    public override string GetAltitude()
    {
        return _altitude;
    }

    /// <summary>
    /// Validate a latitude string.
    /// </summary>
    /// <param name="latitude"></param>
    /// <returns></returns>
    public static bool IsValidLatitude(string latitude)
    {
        if (string.IsNullOrWhiteSpace(latitude))
        {
            return false;
        }

        return
        (
            -90.0 <= double.Parse(latitude)
            && double.Parse(latitude) <= 90.0
        );
    }

    /// <summary>
    /// Validate a longitude string.
    /// </summary>
    /// <param name="longitude"></param>
    /// <returns></returns>
    public static bool IsValidLongitude(string longitude)
    {
        if (string.IsNullOrWhiteSpace(longitude))
        {
            return false;
        }

        return
        (
            -180.0 <= double.Parse(longitude)
            && double.Parse(longitude) <= 180.0
        );
    }

    /// <summary>
    /// Create a Position object from a latitude and longitude.
    /// Leverages abstract "PositionBase" class to return a Position or NullPosition
    /// depending on nullables in input parameters.
    /// </summary>
    /// <param name="lattitude"></param>
    /// <param name="longitude"></param>
    /// <returns></returns>
    public static PositionBase Create(string? lattitude, string? longitude)
    {
        if (string.IsNullOrWhiteSpace(lattitude) || string.IsNullOrWhiteSpace(longitude))
        {
            return new NullPosition();
        }

        string tempLatitude = LimitToFourDecimalPlaces(lattitude);
        string tempLongitude = LimitToFourDecimalPlaces(longitude);

        if (IsValidLatitude(tempLatitude) && IsValidLongitude(tempLongitude))
        {
            return new Position
            {
                _latitude = tempLatitude,
                _longitude = tempLongitude
            };
        }
        else
        {
            return new NullPosition();
        }
    }

    /// <summary>
    /// Create a Position object from a Coordinate instance
    /// limiting latitude and longitude to four decimal places.
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public static string LimitToFourDecimalPlaces(string coordinate)
    {
        string coord = coordinate.Trim();
        string[] coordinateParts = coord.Split('.');
        if (coordinateParts[1].Length > 4)
        {
            return $"{coordinateParts[0]}.{coordinateParts[1].Substring(0, 4)}";
        }
        else
        {
            return coord;
        }
    }

    public override string ToString()
    {
        return $"{_latitude},{_longitude}";
    }
}
