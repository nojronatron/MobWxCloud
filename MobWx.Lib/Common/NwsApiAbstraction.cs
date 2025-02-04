using Microsoft.Extensions.Logging;
using MobWx.Lib.Models;
using System.Text.Json;
using MobWx.Lib.Models.Base;
using System.Text.Json.Serialization;

namespace MobWx.Lib.Common
{
    public class NwsApiAbstraction : INwsApiAbstraction
    {
        private ILogger<NwsApiAbstraction> _logger;
        private IHttpClientFactory _httpClientFactory;

        public NwsApiAbstraction(ILogger<NwsApiAbstraction> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get the relative path for a gridpoint.
        /// </summary>
        /// <param name="gridId"></param>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <returns></returns>
        public static string FoRelativePath(string gridId, string gridX, string gridY)
        {
            return $"/gridpoints/{gridId.ToUpper()}/{gridX},{gridY}/stations";
        }

        /// <summary>
        /// Get the relative path for the latest observation.
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public static string LatestObsPath(string stationId)
        {
            return $"/stations/{stationId}/observations/latest";
        }

        /// <summary>
        /// Get the relative path for a point.
        /// </summary>
        /// <param name="latLon"></param>
        /// <returns></returns>
        public static string PointPath(PositionBase latLon)
        {
            // if latLon is of type NullPosition return an empty string
            if (latLon is NullPosition)
            {
                return string.Empty;
            }
            else
            {
                return $"/points/{latLon.GetLatitude()},{latLon.GetLongitude()}";
            }
        }

        /// <summary>
        /// Get current conditions from NWS API. Depends on other method calls in this class instance.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public async Task<Observation> GetCurrentConditionsAsync(PositionBase position)
        {
            if (position is NullPosition)
            {
                _logger.LogWarning("Position was null. Returning empty string.");
                return new Observation();
            }

            var pointData = await GetPointDataAsync(position);

            if (string.IsNullOrEmpty(pointData))
            {
                _logger.LogWarning("PointData was null. Returning empty string.");
                return new Observation();
            }

            string stationUrl = GetObservationStationsUrl(pointData);

            if (string.IsNullOrEmpty(stationUrl))
            {
                _logger.LogWarning("StationUrl was null. Returning empty string.");
                return new Observation();
            }

            string nearestObsStationsResponse = await GetObservationStationsAsync(stationUrl);

            if (string.IsNullOrEmpty(nearestObsStationsResponse))
            {
                _logger.LogWarning("NearestObStationResponse was null. Returning empty string.");
                return new Observation();
            }

            IEnumerable<string?> observationStations = GetObservationStationsList(nearestObsStationsResponse);

            return await TryGetObservationAsync(observationStations);
        }

        /// <summary>
        /// Try to get observation from NWS API.
        /// </summary>
        /// <param name="observationStations"></param>
        /// <returns></returns>
        public async Task<Observation> TryGetObservationAsync(IEnumerable<string?> observationStations)
        {
            foreach (string? obsStnUrl in observationStations)
            {
                if (string.IsNullOrWhiteSpace(obsStnUrl))
                {
                    _logger.LogWarning("ObsStnUrl was null. Returning empty string.");
                    continue;
                }

                string obsStationId = obsStnUrl.Split('/').Last();
                _logger.LogInformation("Selected station with element: {stnpath} and station ID {stnid}", obsStnUrl, obsStationId);
                var httpClient = _httpClientFactory.CreateClient("NwsApi"); // base: https://api.weather.gov
                var request = new HttpRequestMessage(HttpMethod.Get, LatestObsPath(obsStationId)); // req uri: /stations/{stationId}/observations/latest
                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Fetch returned with a success status code with content length of {contentlength}.", response.Content.Headers.ContentLength);

                    try
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation("Response Content JSON string is: {jsonstring}", jsonString);

                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            Converters = { new JsonStringEnumConverter() }
                        };

                        Observation? observation = JsonSerializer.Deserialize<Observation>(jsonString, options);
                        _logger.LogInformation("JsonSerializer deserialized jsonString to an observation instance.");
                        _logger.LogInformation(jsonString);

                        if (observation is not null)
                        {
                            return observation;
                        }
                        else
                        {
                            _logger.LogWarning("Unable to parse current Observation object. Continuing thread.");
                        }
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError("JSON Deserialization error: {message}, {jsonpath}, {stacktrace}", ex.Message, ex.Path, ex.StackTrace);
                        _logger.LogError("Unable to process data from the NWS NOAA API. Try again later.");
                    }
                }
                else
                {
                    _logger.LogDebug("Unable to fetch observation station url {obsstnurl}", obsStnUrl);
                    _logger.LogError("Unable to process data from the NWS NOAA API. Try again later.");
                }
            }

            _logger.LogDebug("Returning an empty Observation instance.");
            return new Observation();
        }

        /// <summary>
        /// Get observation stations list from NWS API.
        /// </summary>
        /// <param name="objStationsUrlListJson"></param>
        /// <returns></returns>
        public IEnumerable<string?> GetObservationStationsList(string objStationsUrlListJson)
        {
            try
            {
                var jsonDocument = JsonDocument.Parse(objStationsUrlListJson);
                if (jsonDocument.RootElement.TryGetProperty("observationStations", out JsonElement observationStationsElement))
                {
                    return observationStationsElement.EnumerateArray().Select(x => x.GetString());
                }
            }
            catch (JsonException ex)
            {
                _logger.LogDebug($"Failed to parse observation stations from array of URLs: {ex.Message}");
                _logger.LogError($"Unable to process data from the NWS NOAA API. Try again later.");
            }

            _logger.LogDebug("Returning an empty Enumerable string.");
            return new List<string>();
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

            _logger.LogDebug($"Failed to get observation stations from {url}");
            _logger.LogError($"Unable to process data from the NWS NOAA API. Try again later.");
            return string.Empty;
        }

        /// <summary>
        /// Get observation stations URL from points data.
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public string GetObservationStationsUrl(string points)
        {
            try
            {
                var jsonDocument = JsonDocument.Parse(points);
                if (jsonDocument.RootElement.TryGetProperty("observationStations", out JsonElement stationsElement))
                {
                    return stationsElement.GetString() ?? string.Empty;
                }
            }
            catch (JsonException ex)
            {
                _logger.LogDebug($"Failed to parse observation stations from points: {ex.Message}");
                _logger.LogError($"Unable to process data from the NWS NOAA API. Try again later.");
            }

            _logger.LogDebug("Returning an empty string.");
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
            var request = new HttpRequestMessage(HttpMethod.Get, PointPath(position));
            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            _logger.LogDebug($"Failed to get point data for {position.GetLatitude()}, {position.GetLongitude()}");
            _logger.LogError($"Unable to process data from the NWS NOAA API. Try again later.");
            return string.Empty;
        }
    }
}
