namespace MobWx.Lib.Models.Geocoding;

public static class NominatimExtensions
{
    /// <summary>
    /// Safely Checks whether this instance has a license, coordinate doubles, and city, county, state, and country.
    /// </summary>
    /// <param name="geocodedResponse"></param>
    /// <returns>True if all properties have value, otherwise false.</returns>
    public static bool IsValidNominatimResponse(this NominatimGeocodeResponse geocodedResponse)
    {
        // return true if the geocoded response has the following: license, name, display_name, and an array of at least 2 doubles called coordinates
        return 
            (
            HasLicense(geocodedResponse) &&
            HasCoordinateDoubles(geocodedResponse) &&
            HasCityStateCountry(geocodedResponse)
            );
    }

    /// <summary>
    /// Safely checks whether this instance has at least one pair of doubles in the Geometry property.
    /// </summary>
    /// <param name="geocodedResponse"></param>
    /// <returns>True if found, otherwise false.</returns>
    public static bool HasCoordinateDoubles(this NominatimGeocodeResponse geocodedResponse)
    {
        try
        {
            return geocodedResponse.Features.FirstOrDefault()?.Geometry?.CoordinateDoubles.Count > 1;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Safely checks whether this instance has both Name and DisplayName properties the first Features array.
    /// </summary>
    /// <param name="geocodedResponse"></param>
    /// <returns>True if found, otherwise false.</returns>
    public static bool HasCityStateCountry(this NominatimGeocodeResponse geocodedResponse)
    {
        try
        {
            var cityName = geocodedResponse.Features[0]?.FeatureProperties?.Name;
            var displayName = geocodedResponse.Features[0]?.FeatureProperties?.DisplayName;

            return (
                false == string.IsNullOrWhiteSpace(cityName)
                || false == string.IsNullOrWhiteSpace(displayName)
            );
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Safely checks whether this instance has a license statement.
    /// </summary>
    /// <param name="geocodedResponse"></param>
    /// <returns>True if found, otherwise false.</returns>
    public static bool HasLicense(this NominatimGeocodeResponse geocodedResponse)
    {
        try
        {
            return (
                false == string.IsNullOrWhiteSpace(geocodedResponse.License)
                );
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Checks for the Latitude value found in the Geometry property, if it exists.
    /// </summary>
    /// <param name="geocodedResponse"></param>
    /// <returns>The nullable double value.</returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if Features is null or empty.</exception>
    public static double? GetLatitude(this NominatimGeocodeResponse geocodedResponse)
    {
        return geocodedResponse.Features.FirstOrDefault()?.Geometry?.CoordinateDoubles[1];
    }

    /// <summary>
    /// Checks for the Longitude value found in the Geometry property, if it exists.
    /// </summary>
    /// <param name="geocodedResponse"></param>
    /// <returns>The nullable double value.</returns>
    /// <exception cref="ArgumentNullException">Throws ArgumentNullException if Features is null or empty.</exception>
    public static double? GetLongitude(this NominatimGeocodeResponse geocodedResponse)
    {
        return geocodedResponse.Features.FirstOrDefault()?.Geometry?.CoordinateDoubles[0];
    }

    /// <summary>
    /// Safely creates a new Position instances from a geocoded response instance.
    /// Only includes existing Coordinates and/or Location instances if enough properties exist.
    /// </summary>
    /// <param name="geocodedResponse"></param>
    /// <returns>A valid Position instance. Instance will be empty if not enough Coordinate or Location properties were found.</returns>
    public static Position ToPosition(this NominatimGeocodeResponse geocodedResponse)
    {
        if (geocodedResponse.IsValidNominatimResponse())
        {
            return Position.Create(
                ToCoordinate(geocodedResponse),
                ToLocation(geocodedResponse)
                );
        }

        if (geocodedResponse.HasCoordinateDoubles())
        {
            return Position.Create(
                ToCoordinate(geocodedResponse)
                );
        }

        if (geocodedResponse.HasCityStateCountry() && geocodedResponse.HasLicense())
        {
            return Position.Create(
                ToLocation(geocodedResponse)
                );
        }
            
        return new Position();
    }

    /// <summary>
    /// Safely creates a new Coordinate instance from a geocoded response instance.
    /// </summary>
    /// <param name="geocodedResponse"></param>
    /// <returns>A valid Coordinate instance, or empty instance if missing Latitude and/or Longitude properties.</returns>
    public static Coordinate ToCoordinate(this NominatimGeocodeResponse geocodedResponse)
    {
        try
        {
            return Coordinate.Create(
                (double)GetLatitude(geocodedResponse)!,
                (double)GetLongitude(geocodedResponse)!
            );
        }
        catch
        {
            return new Coordinate();
        }
    }

    /// <summary>
    /// Safely creates a new Location instance from a geocoded response instance.
    /// </summary>
    /// <param name="geocodedResponse"></param>
    /// <returns>A valid Location instance, or empty instance if missing City, State, Country, or License properties.</returns>
    public static Location ToLocation(this NominatimGeocodeResponse geocodedResponse)
    {
        if (geocodedResponse.HasCityStateCountry() && geocodedResponse.HasLicense())
        {
            string city = geocodedResponse.Features[0]!.FeatureProperties!.Name!.Trim();
            string displayName = geocodedResponse.Features[0]!.FeatureProperties!.DisplayName!.Trim();
            string state = displayName!.Split(",")[2].Trim();
            var license = geocodedResponse.License;

            if (
                string.IsNullOrWhiteSpace(state)
                )
            {
                return new Location();
            }

            return Location.Create(city, state, license, displayName);
        }
        else
        {
            return new Location();
        }
    }
}
