namespace MobWx.Lib.Models.Base;

/// <summary>
/// Supports Position and NullPosition objects.
/// </summary>
public class PositionBase
{
    public string Latitude { get; protected set; }
    public string Longitude { get; protected set; }
    public string Altitude { get; protected set; } = string.Empty;

    protected PositionBase(string latitude, string longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static PositionBase Create(string? latitude, string? longitude)
    {
        if (string.IsNullOrWhiteSpace(latitude) || string.IsNullOrWhiteSpace(longitude))
        {
            return new NullPosition();
        }

        string tempLatitude = LimitToFourDecimalPlaces(latitude);
        string tempLongitude = LimitToFourDecimalPlaces(longitude);

        if (IsValidLatitude(tempLatitude) && IsValidLongitude(tempLongitude))
        {
            return new Position(tempLatitude, tempLongitude);
        }
        else
        {
            return new NullPosition();
        }
    }

    public static bool IsValidLatitude(string latitude)
    {
        if (string.IsNullOrWhiteSpace(latitude))
        {
            return false;
        }

        return -90.0 <= double.Parse(latitude) && double.Parse(latitude) <= 90.0;
    }

    public static bool IsValidLongitude(string longitude)
    {
        if (string.IsNullOrWhiteSpace(longitude))
        {
            return false;
        }

        return -180.0 <= double.Parse(longitude) && double.Parse(longitude) <= 180.0;
    }

    public static string LimitToFourDecimalPlaces(string coordinate)
    {
        string coord = coordinate.Trim();
        string[] coordinateParts = coord.Split('.');
        if (coordinateParts.Length > 1 && coordinateParts[1].Length > 4)
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
        return $"{Latitude},{Longitude}";
    }
}
