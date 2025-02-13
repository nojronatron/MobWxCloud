using System.ComponentModel.DataAnnotations;

namespace MobWx.Lib.Models.Geocoding;

public class Location(double lat, double lon) : Coordinate(lat, lon)
{
    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public string? CityName { get; set; }

    [Required]
    [StringLength(maximumLength: 3, MinimumLength = 2)]
    public string? StateAbbreviation { get; set; }

    public string? Country { get; } = "United States";

    public override bool Equals(object? obj)
    {
        if (obj is Location location)
        {
            return
                CityName == location.CityName &&
                StateAbbreviation == location.StateAbbreviation &&
                Country == location.Country &&
                Lat == location.Lat &&
                Lon == location.Lon;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Lat, Lon, CityName, StateAbbreviation, Country);
    }
}
