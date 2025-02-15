using MobWx.API.Common;
using MobWx.Lib.Models.Geocoding;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MobWx.API.Endpoints;

/// <summary>
/// Handles Geolocation data between MobWxAPI and OpenStreetMaps endpoints.
/// </summary>
public class LocationHandler : ILocationHandler
{
    private readonly ILogger<ForecastsHandler> _logger;
    private readonly IJsonHandler _jsonHandler;
    private readonly IOpenStreetMapsAbstraction _openStreetMapsAbstraction;

    public LocationHandler(
        ILogger<ForecastsHandler> logger,
        IJsonHandler jsonHandler,
        IOpenStreetMapsAbstraction openStreetMapsAbstraction
        )
    {
        _logger = logger;
        _jsonHandler = jsonHandler;
        _openStreetMapsAbstraction = openStreetMapsAbstraction;
    }

    /// <summary>
    /// Attempts to parse the city and state from the request.
    /// </summary>
    /// <param name="city"></param>
    /// <param name="state"></param>
    /// <param name="cleanCity"></param>
    /// <param name="cleanState"></param>
    /// <returns></returns>
    public bool TryParseLocation(string city, string state, out string cleanCity, out string cleanState)
    {
        cleanCity = string.Empty;
        cleanState = string.Empty;

        string pattern = @"^[a-zA-Z\s]+$";

        try
        {
            var cityMatch = Regex.Match(city, pattern);

            if (cityMatch.Success == false)
            {
                return false;
            }

            var stateMatch = Regex.Match(state, pattern);

            if (stateMatch.Success == false)
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning("An exception occurred while parsing City or State: {exception}", ex.StackTrace);
        }

        cleanCity = city.Trim();
        cleanState = state.Trim();
        _logger.LogInformation("City and State were parsed without error, returning {city}, {state}.", cleanCity, cleanState);
        return true; ;
    }

    /// <summary>
    /// Gets the geolocation data for a given city and state.
    /// </summary>
    /// <param name="city"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public async Task<IResult> GetGeoLocationAsync(string city, string state)
    {
        if (TryParseLocation(city, state, out string cleanCity, out string cleanState))
        {

            Location location = new(0d, 0d);
            location.CityName = cleanCity;
            location.StateAbbreviation = cleanState;
            var geolocationResponse = await _openStreetMapsAbstraction.GetGeoJsonDataAsync(location);

            if (geolocationResponse is null)
            {
                return Results.NotFound();
            }

            var geolocation = _jsonHandler.TryDeserializeGeocodeResponse(geolocationResponse);

            if (geolocation is null)
            {
                return Results.Problem("Failed to deserialize geolocation data from OpenStreetMaps.");
            }

            if (geolocation.Features.Count < 1)
            {
                return Results.Problem("Failed to get geolocation features from OpenStreetMaps.");
            }

            if (geolocation.Features[0].Geometry is null)
            {
                return Results.Problem("Failed to get geolocation feature geometry from OpenStreetMaps.");
            }

            if (geolocation.Features[0].Geometry!.CoordinateDoubles.Count < 2)
            {
                return Results.Problem("Failed to get geolocation feature geometry coordinates from OpenStreetMaps.");
            }

            try
            {
                Location latLonOnly = geolocation.Features[0].Geometry!.GetLocation();
                location.Lat = latLonOnly.Lat;
                location.Lon = latLonOnly.Lon;
                var output = JsonSerializer.Serialize(location, new JsonSerializerOptions { WriteIndented = true });
                return Results.Ok(location);
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occurred while attempting to get the location from the geolocation data: {exception}", ex.StackTrace);
                return Results.Problem("Bad programmer! Failed to get geolocation features geometry location, probably due to nulls.");
            }
        }
        else
        {
            return Results.BadRequest("City and State must be included in the request.");
        }
    }
}
