using MobWx.Lib.ForecastModels;
using MobWx.Lib.Models;
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

    public ForecastResponse? TryDeserializeForecastResponseAsync(string forecastResponseJson)
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
}
