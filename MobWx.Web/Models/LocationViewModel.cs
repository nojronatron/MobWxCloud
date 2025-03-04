using System.Text.Json.Serialization;

namespace MobWx.Web.Models;

public class LocationViewModel
{
    [JsonPropertyName("cityName")]
    public string City { get; set; }

    [JsonPropertyName("stateAbbreviation")]
    public string State { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("lat")]
    public double Latitude { get; set; }

    [JsonPropertyName("lon")]
    public double Longitude { get; set; }
}
