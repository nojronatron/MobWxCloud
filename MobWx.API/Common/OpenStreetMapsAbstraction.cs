using MobWx.Lib.Models.Geocoding;

namespace MobWx.API.Common;

/// <summary>
/// Presents OpenStreenMaps geocode REST and JSON processing abstractions.
/// </summary>
public class OpenStreetMapsAbstraction : IOpenStreetMapsAbstraction
{
    private readonly ILogger<OpenStreetMapsAbstraction> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public OpenStreetMapsAbstraction(ILogger<OpenStreetMapsAbstraction> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Fetches geocoding data from OpenStreetMaps.
    /// </summary>
    /// <param name="city"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public async Task<string> GetGeoJsonDataAsync(Location location)
    {
        var httpClient = _httpClientFactory.CreateClient("OsmApi");
        var request = new HttpRequestMessage(HttpMethod.Get, OsmEndpointPaths.GetGeocodeRequestUrl(location.CityName!, location.StateAbbreviation!));

        try
        {
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Fetch returned with a success status code with content length of {contentlength}.", response.Content.Headers.ContentLength);
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                _logger.LogWarning("Request for Geolocation data for {cityname}, {statename} failed. A non-success status code was returned.", location.CityName, location.StateAbbreviation);
            }
        }
        catch (ArgumentNullException anex)
        {
            _logger.LogError("An argument was null. Observation data will not be fetched: {exception}", anex.StackTrace);
        }
        catch (InvalidOperationException ioex)
        {
            _logger.LogError("An invalid operation was attempted before or while requesting data for {cityname}, {statename}: {exception}", location.CityName, location.StateAbbreviation, ioex.StackTrace);
        }
        catch (HttpRequestException hrex)
        {
            _logger.LogError("An exception occurred while requesting data for {cityname}, {statename}: {exception}", location.CityName, location.StateAbbreviation, hrex.StackTrace);
        }
        catch (TaskCanceledException tcex)
        {
            _logger.LogError("The fetch data task for {cityname}, {statename} was canceled. {exception}", location.CityName, location.StateAbbreviation, tcex.StackTrace);
        }
        catch (Exception ex)
        {
            _logger.LogError("An exception occurred fetching geocoding data for {cityname}, {statename}: {exception}", location.CityName, location.StateAbbreviation, ex.StackTrace);
        }

        return string.Empty;
    }
}
