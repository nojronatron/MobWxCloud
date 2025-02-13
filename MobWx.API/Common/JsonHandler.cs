using MobWx.Lib.ForecastModels;
using MobWx.Lib.Models;
using MobWx.Lib.Models.Geocoding;
using MobWx.Lib.NwsAlertModels;
using MobWx.Lib.PointModels;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MobWx.API.Common;

public class JsonHandler : IJsonHandler
{
    private readonly ILogger<JsonHandler> _logger;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public JsonHandler(ILogger<JsonHandler> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Deserializes PointsResponse JSON into a PointsResponse object.
    /// </summary>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public PointsResponse? TryDeserializePointsResponse(string jsonString)
    {
        try
        {
            return JsonSerializer.Deserialize<PointsResponse>(jsonString, _jsonOptions);
        }
        catch (JsonException ex)
        {
            _logger.LogError("Failed to deserialize PointsResponse instance: {message}, {jsonpath}, {stacktrace}", ex.Message, ex.Path, ex.StackTrace);
        }

        return null;
    }

    /// <summary>
    /// Deserializes Location JSON into a Location object.
    /// </summary>
    /// <param name="geoJsonData"></param>
    /// <returns></returns>
    public GeocodeResponse? TryDeserializeGeocodeResponse(string geoJsonData)
    {
        try
        {
            _logger.LogInformation("About to attempt deserialization of the following json string: {jsonstring}", geoJsonData);
            return JsonSerializer.Deserialize<GeocodeResponse>(geoJsonData, _jsonOptions);
        }
        catch (JsonException jex)
        {
            _logger.LogError("Failed to deserialize Location instance: {message}, {jsonpath}, {stacktrace}", jex.Message, jex.Path, jex.StackTrace);
        }
        catch (Exception ex)
        {
            _logger.LogError("An unexpected Exception was thrown: {message}, {stacktrace}", ex.Message, ex.StackTrace);
        }

        return null;
    }

    /// <summary>
    /// Deserializes ForecastResponse JSON into a ForecastResponse object.
    /// </summary>
    /// <param name="forecastResponseJson"></param>
    /// <returns></returns>
    public ForecastResponse? TryDeserializeForecastResponse(string forecastResponseJson)
    {
        try
        {
            return JsonSerializer.Deserialize<ForecastResponse>(forecastResponseJson, _jsonOptions);
        }
        catch (JsonException ex)
        {
            _logger.LogError("Failed to deserialize ForecastResponse instance: {message}, {jsonpath}, {stacktrace}", ex.Message, ex.Path, ex.StackTrace);
        }

        return null;
    }

    /// <summary>
    /// Deserializes Observation JSON into an Observation object.
    /// </summary>
    /// <param name="jsonString"></param>
    /// <returns></returns>
    public Observation? TryDeserializeObservation(string jsonString)
    {
        try
        {
            return JsonSerializer.Deserialize<Observation>(jsonString, _jsonOptions);
        }
        catch (JsonException ex)
        {
            _logger.LogError("Failed to deserialize Observation instance: {message}, {jsonpath}, {stacktrace}", ex.Message, ex.Path, ex.StackTrace);
        }

        return null;
    }

    /// <summary>
    /// Extracts observation stations from a JSON array of URLs.
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
            _logger.LogError("Failed to parse observation stations list from array of URLs: {exmessage}", ex.Message);
        }

        return new List<string>();
    }

    /// <summary>
    /// Extracts the observation stations URL from a JSON object.
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
            _logger.LogError("Failed to parse observation stations from points: {exmessage}", ex.Message);
        }

        return string.Empty;
    }

    /// <summary>
    /// Deserializes ActiveAlerts JSON into an ActiveAlerts object.
    /// </summary>
    /// <param name="activeAlertJson"></param>
    /// <returns></returns>
    public ActiveAlerts? TryDeserializeActiveAlerts(string activeAlertJson)
    {
        try
        {
            return JsonSerializer.Deserialize<ActiveAlerts>(activeAlertJson, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to Deserialize ActiveAlerts: {exStackTrace}", ex.StackTrace);
        }

        return null;
    }
}
