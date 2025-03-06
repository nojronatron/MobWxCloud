using System.ComponentModel.DataAnnotations;

namespace MobWx.Lib.Models.Geocoding;

public class Location : IEquatable<Location>
{
    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public string? CityName { get; set; }

    [Required]
    [StringLength(maximumLength: 3, MinimumLength = 2)]
    public string? StateAbbreviation { get; set; }

    public string? Country { get; } = "United States";
    public string License { get; set; } = string.Empty; // from Nominatim API
    public string DisplayName { get; set;} = string.Empty; // from Nominatim API

    public string GetStateName()
    {
        if (string.IsNullOrWhiteSpace(License))
        {
            return string.Empty;
        }

        string[] items = DisplayName.Split(",");
        return items[items.Length - 2].Trim();
    }

    /// <summary>
    /// Create a concrete Location instance.
    /// </summary>
    /// <param name="city"></param>
    /// <param name="state"></param>
    /// <param name="license"></param>
    /// <param name="displayName"></param>
    /// <returns></returns>
    public static Location Create(string city, string state, string license, string displayName)
    {
        return new Location
        {
            CityName = city,
            StateAbbreviation = state,
            License = license,
            DisplayName = displayName
        };
    }

    /// <summary>
    /// Determines if the current Location object is equal to another Location object.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Location? other)
    {
        if (string.IsNullOrWhiteSpace(CityName) || string.IsNullOrWhiteSpace(StateAbbreviation) || other is null)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(other.CityName) || string.IsNullOrWhiteSpace(other.StateAbbreviation))
        {
            return false;
        }

        return
            CityName == other.CityName
            && StateAbbreviation == other.StateAbbreviation
            && Country == other.Country;
    }

    /// <summary>
    /// Determines if the current Location object is equal to another object.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return Equals(obj as Location);
    }

    /// <summary>
    /// Returns the hash code for the current Location object.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(CityName, StateAbbreviation, Country);
    }
}
