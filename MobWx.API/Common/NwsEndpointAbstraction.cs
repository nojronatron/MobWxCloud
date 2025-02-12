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

            var observationHttpClient = _httpClientFactory.CreateClient("NwsApi");
            var request = new HttpRequestMessage(HttpMethod.Get, NwsEndpointPaths.LatestObsPath(obsStationId));

            try
            {
                var response = await observationHttpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Fetch returned with a success status code with content length of {contentlength}.", response.Content.Headers.ContentLength);
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    _logger.LogWarning("Request for Observation Station {obsstnid} failed. A non-success status code was returned.", obsStationId);
                }
            }
            catch (ArgumentNullException anex)
            {
                _logger.LogError("An argument was null. Observation data will not be fetched: {exception}", anex.StackTrace);
            }
            catch (InvalidOperationException ioex)
            {
                _logger.LogError("An invalid operation was attempted before or while requesting observation data from {obsstnid}: {exception}", obsStationId, ioex.StackTrace);
            }
            catch (HttpRequestException hrex)
            {
                _logger.LogError("An exception occurred while requesting data from Observation Station {osbstnid}: {exception}", obsStationId, hrex.StackTrace);
            }
            catch (TaskCanceledException tcex)
            {
                _logger.LogError("The fetch data task for station {obsstnid} was canceled. {exception}", obsStationId, tcex.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occurred calling {obsstnid}: {exception}", obsStationId, ex.StackTrace);
            }

            return "Unable to fetch data from the NWS NOAA API. Try again later or use a different location.";
        }

        /// <summary>
        /// Get observation stations string from NWS API.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> GetObservationStationsAsync(string url)
        {
            var observationStationsHttpClient = _httpClientFactory.CreateClient("NwsElementUrl");
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await observationStationsHttpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            _logger.LogDebug("Failed to get observation stations from {url}", url);
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
