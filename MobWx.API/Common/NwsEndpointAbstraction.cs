using MobWx.Lib.Models;
using MobWx.Lib.Models.Base;
using MobWx.Lib.PointModels;

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
        /// Fetches the Points metadata from the NWS API.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public async Task<string> GetNwsPointsAsync(Position position)
        {
            _logger.LogInformation("Fetching Points metadata for (lat, lon): ({position}).", position);

            var pointsHttpClient = _httpClientFactory.CreateClient("NwsApi");
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                NwsEndpointPaths.PointPath((Position)position)
                );
            var pointsResponse = await pointsHttpClient.SendAsync(request);

            if (pointsResponse.IsSuccessStatusCode)
            {
                string pointsResponseJson = await pointsResponse.Content.ReadAsStringAsync();
                _logger.LogInformation("Received Points metadata: {pointjson}", pointsResponseJson);
                return pointsResponseJson;
            }

            return string.Empty;
        }

        /// <summary>
        /// Fetches the Forecast data from the NWS API.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public async Task<string> GetNwsForecastsAsync(PointsResponse points)
        {
            var forecastHttpClient = _httpClientFactory.CreateClient("NwsElementUrl");
            var request = new HttpRequestMessage(
                HttpMethod.Get, points.Forecast
                );
            var forecastResponse = await forecastHttpClient.SendAsync(request);

            if (forecastResponse.IsSuccessStatusCode)
            {
                string forecastJson = await forecastResponse.Content.ReadAsStringAsync();
                _logger.LogInformation("Fetch returned Forecast json: {forecastjson}", forecastJson);
                return forecastJson;
            }
            else
            {
                _logger.LogWarning("NWS API did not respond or the response for a forecast was empty. Try again later or try a different location.");
            }

            return string.Empty;
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

        /// <summary>
        /// Fetches active alerts from the NWS API for a given position. Awaitable.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public async Task<string> GetNwsAlertsAsync(PositionBase position)
        {
            var httpClient = _httpClientFactory.CreateClient("NwsApi");
            var request = new HttpRequestMessage(
                HttpMethod.Get, NwsEndpointPaths.GetActiveAlertPath((Position)position)
                );
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string activeAlertsJson = await response.Content.ReadAsStringAsync();
                _logger.LogInformation("Received active alerts JSON: {activeAlertJson}", activeAlertsJson);
                return activeAlertsJson;
            }

            return string.Empty;
        }
    }
}
