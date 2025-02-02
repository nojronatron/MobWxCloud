namespace MobWx.Lib.Models;

using System.Text.Json.Serialization;

public partial class Observation
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("station")]
    public string Station { get; set; } = string.Empty;

    [JsonPropertyName("timestamp")]
    public DateTime? Timestamp { get; set; }

    [JsonPropertyName("rawMessage")]
    public string RawMessage { get; set; } = string.Empty;

    [JsonPropertyName("textDescription")]
    public string TextDescription { get; set; } = string.Empty;

    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;

    [JsonPropertyName("temperature")]
    public Measurement? Temperature { get; set; }

    [JsonPropertyName("dewpoint")]
    public Measurement? Dewpoint { get; set; }

    [JsonPropertyName("windDirection")]
    public Measurement? WindDirection { get; set; }

    [JsonPropertyName("windSpeed")]
    public Measurement? WindSpeed { get; set; }

    [JsonPropertyName("windGust")]
    public Measurement? WindGust { get; set; }

    [JsonPropertyName("barometricPressure")]
    public Measurement? BarometricPressureMb { get; set; }

    [JsonPropertyName("visibility")]
    public Measurement? Visibility { get; set; }

    [JsonPropertyName("maxTemperatureLast24Hours")]
    public Measurement? MaxTemperatureLast24Hours { get; set; }

    [JsonPropertyName("minTemperatureLast24Hours")]
    public Measurement? MinTemperatureLast24Hours { get; set; }

    [JsonPropertyName("precipitationLastHour")]
    public Measurement? PrecipitationLastHour { get; set; }

    [JsonPropertyName("windChill")]
    public Measurement? WindChill { get; set; }

    [JsonPropertyName("heatIndex")]
    public Measurement? HeatIndex { get; set; }

    [JsonPropertyName("cloudLayers")]
    public List<CloudLayer>? CloudLayers { get; set; }
}
