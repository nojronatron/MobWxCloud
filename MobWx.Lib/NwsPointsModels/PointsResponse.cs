using MobWx.Lib.Models;
using System.Text.Json.Serialization;

namespace MobWx.Lib.PointModels
{
    public class PointsResponse
    {
        [JsonPropertyName("@id")]
        public string? Id { get; set; }

        [JsonPropertyName("forecastOffice")]
        public string? ForecastOfficeUrl { get; set; } // this is an API url

        [JsonPropertyName("gridId")]
        public string? GridId { get; set; } // aka CWA aka NWS Forecast Office ID

        [JsonPropertyName("gridX")]
        public int? GridX { get; set; } // NWS GridId X

        [JsonPropertyName("gridY")]
        public int? GridY { get; set; } // NWS GridId Y

        [JsonPropertyName("forecast")]
        public string? Forecast { get; set; } // full nwsapi url to /forecast

        [JsonPropertyName("forecastHourly")]
        public string? ForecastHourly { get; set; } // full nwsapi url to /forecast/hourly

        [JsonPropertyName("forecastGridData")]
        public string? TabularForecastData { get; set; } // full nwsapi url to hourly forecast values (for tabular, graph display)

        [JsonPropertyName("observationStations")]
        public string? ObservationStations { get; set; } // full nwsapi url to /stations

        [JsonPropertyName("relativeLocation")]
        public RelativeLocation? RelativeLocation { get; set; }

        [JsonPropertyName("forecastZone")]
        public string? ForecastZone { get; set; } // full nwsapi url to /zones/forecast/{zone}

        [JsonPropertyName("county")]
        public string? County { get; set; } // full nwsapi url to /??

        [JsonPropertyName("fireWeatherZone")]
        public string? FireWeatherZone { get; set; } // full nwsapi to /??

        [JsonPropertyName("timeZone")]
        public string? TimeZone { get; set; } // plain-text timezone e.g. "America/Los_Angeles"

        [JsonPropertyName("radarStation")]
        public string? RadarStation { get; set; } // e.g. "KATX"

        public string NwsForecastOfficeId => GridId ?? string.Empty;
    }
}
