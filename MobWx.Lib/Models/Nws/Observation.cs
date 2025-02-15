using System.Text.Json.Serialization;

namespace MobWx.Lib.Models.Nws;

public partial class Observation
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("geometry")]
    public string? Geometry { get; set; } // nwsapi doc is wrong this is a string

    [JsonPropertyName("elevation")]
    public QuantitativeValue? StationElevationM { get; set; } // elevation of the obs station om meters

    [JsonPropertyName("station")]
    public string Station { get; set; } = string.Empty; // api.weather.gov/stations/{station}

    [JsonPropertyName("timestamp")]
    public DateTime? Timestamp { get; set; }

    [JsonPropertyName("rawMessage")]
    public string RawMessage { get; set; } = string.Empty;

    [JsonPropertyName("textDescription")]
    public string TextDescription { get; set; } = string.Empty;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty; // api.weather.gov/icons/{area}/{dayNite}/{sky}?size={size}

    [JsonPropertyName("temperature")]
    public QuantitativeValue? TemperatureC { get; set; }

    [JsonPropertyName("dewpoint")]
    public QuantitativeValue? DewpointC { get; set; }

    [JsonPropertyName("windDirection")]
    public QuantitativeValue? WindDirection { get; set; }

    [JsonPropertyName("windSpeed")]
    public QuantitativeValue? WindSpeedKph { get; set; }

    [JsonPropertyName("windGust")]
    public QuantitativeValue? WindGustKph { get; set; }

    [JsonPropertyName("barometricPressure")]
    public QuantitativeValue? BarometricPressurePa { get; set; }

    [JsonPropertyName("visibility")]
    public QuantitativeValue? VisibilityM { get; set; }

    [JsonPropertyName("maxTemperatureLast24Hours")]
    public QuantitativeValue? MaxTempCLast24Hours { get; set; }

    [JsonPropertyName("minTemperatureLast24Hours")]
    public QuantitativeValue? MinTempCLast24Hours { get; set; }

    [JsonPropertyName("precipitationLastHour")]
    public QuantitativeValue? PrecipitationLastHourMm { get; set; }

    [JsonPropertyName("relativeHumidity")]
    public QuantitativeValue? RhPercent { get; set; }

    [JsonPropertyName("windChill")]
    public QuantitativeValue? WindChillC { get; set; }

    [JsonPropertyName("heatIndex")]
    public QuantitativeValue? HeatIndexC { get; set; }

    [JsonPropertyName("cloudLayers")]
    public List<CloudLayer>? CloudLayers { get; set; }
}
