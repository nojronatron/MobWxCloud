using System.Text.Json.Serialization;

namespace MobWx.Lib.Models;

public partial class Observation
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("geometry")]
    public string? PointLocation { get; set; } // note long before lat: "POINT(-123.45 67.89)"

    [JsonPropertyName("elevation")]
    public Elevation? StationElevation { get; set; } // elevation of the obs station om meters

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
    public QuantitativeValue? Temperature { get; set; }

    [JsonPropertyName("dewpoint")]
    public QuantitativeValue? Dewpoint { get; set; }

    [JsonPropertyName("windDirection")]
    public QuantitativeValue? WindDirection { get; set; }

    [JsonPropertyName("windSpeed")]
    public QuantitativeValue? WindSpeed { get; set; }

    [JsonPropertyName("windGust")]
    public QuantitativeValue? WindGust { get; set; }

    [JsonPropertyName("barometricPressure")]
    public QuantitativeValue? BarometricPressureHpa { get; set; }

    [JsonPropertyName("visibility")]
    public QuantitativeValue? Visibility { get; set; }

    [JsonPropertyName("maxTemperatureLast24Hours")]
    public QuantitativeValue? MaxTemperatureLast24Hours { get; set; }

    [JsonPropertyName("minTemperatureLast24Hours")]
    public QuantitativeValue? MinTemperatureLast24Hours { get; set; }

    [JsonPropertyName("precipitationLastHour")]
    public QuantitativeValue? PrecipitationLastHour { get; set; }

    [JsonPropertyName("relativeHumidity")]
    public QuantitativeValue? RelativeHumidity { get; set; }

    [JsonPropertyName("windChill")]
    public QuantitativeValue? WindChill { get; set; }

    [JsonPropertyName("heatIndex")]
    public QuantitativeValue? HeatIndex { get; set; }

    [JsonPropertyName("cloudLayers")]
    public List<CloudLayer>? CloudLayers { get; set; }
}
