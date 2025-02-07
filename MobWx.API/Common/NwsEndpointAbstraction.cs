using MobWx.Lib.Models;
using MobWx.Lib.Models.Base;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MobWx.API.Common
{
    public class NwsEndpointAbstraction : INwsEndpointAbstraction
    {
        private readonly ILogger<NwsEndpointAbstraction> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public NwsEndpointAbstraction(
            ILogger<NwsEndpointAbstraction> logger, 
            IHttpClientFactory httpClientFactory
            )
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Try to get observation from NWS API.
        /// </summary>
        /// <param name="observationStations"></param>
        /// <returns></returns>
        public async Task<string> TryGetObservationAsync(string observationStationUrl)
        {
            string obsStationId = observationStationUrl.Split('/').Last();
            _logger.LogInformation("Selected station with element: {stnpath} and station ID {stnid}", observationStationUrl, obsStationId);

            var httpClient = _httpClientFactory.CreateClient("NwsApi"); // base: https://api.weather.gov
            var request = new HttpRequestMessage(HttpMethod.Get, NwsEndpointPaths.LatestObsPath(obsStationId)); // req uri: /stations/{stationId}/observations/latest
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Fetch returned with a success status code with content length of {contentlength}.", response.Content.Headers.ContentLength);
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                _logger.LogError("Unable to fetch observation url to Observation Station {obsstnid}", obsStationId);
                return "Unable to fetch data from the NWS NOAA API. Try again later.";
            }
        }

        /// <summary>
        /// Get observation stations string from NWS API.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> GetObservationStationsAsync(string url)
        {
            var httpClient = _httpClientFactory.CreateClient("NwsElementUrl");
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            _logger.LogDebug("Failed to get observation stations from {url}", url);
            _logger.LogError("Unable to process data from the NWS NOAA API. Try again later.");
            return string.Empty;
        }

        /// <summary>
        /// Get point data from NWS API.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public async Task<string> GetPointDataAsync(PositionBase position)
        {
            var httpClient = _httpClientFactory.CreateClient("NwsApi");
            var request = new HttpRequestMessage(HttpMethod.Get, NwsEndpointPaths.PointPath(position));
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            _logger.LogDebug("Failed to get point data for {positionlongitude}, {positionlongitude}", position.Longitude, position.Longitude);
            _logger.LogError("Unable to process data from the NWS NOAA API. Try again later.");
            return string.Empty;
        }
    }
}
