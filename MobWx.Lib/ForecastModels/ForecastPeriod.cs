using MobWx.Lib.Models;
using System.Text.Json.Serialization;

namespace MobWx.Lib.ForecastModels
{
    public class ForecastPeriod
    {
        [JsonPropertyName("number")]
        public int? Number { get; set; } // ordinal 1-14 for a 7 day forecast split into 12hr periods

        [JsonPropertyName("name")]
        public string? Name { get; set; } // will be null for hourly

        [JsonPropertyName("startTime")]
        public DateTimeOffset? StartTime { get; set; }

        [JsonPropertyName("endTime")]
        public DateTimeOffset? EndTime { get; set; }

        [JsonPropertyName("isDaytime")]
        public bool IsDaytime { get; set; } = true;

        [JsonPropertyName("temperature")]
        public int Temperature { get; set; } = int.MinValue;

        [JsonPropertyName("temperatureUnit")]
        public string? TemperatureUnit { get; set; }

        [JsonPropertyName("temperatureTrend")]
        public string? TemperatureTrend { get; set; } // rising or falling (or null?)

        [JsonPropertyName("probabilityOfPrecipitation")]
        public QuantitativeValue? ProbabilityOfPrecipitation { get; set; } = new();

        [JsonPropertyName("dewpoint")]
        public QuantitativeValue? Dewpoint { get; set; } = new();

        [JsonPropertyName("relativeHumidity")]
        public QuantitativeValue? RelativeHumidity { get; set; } = new();

        [JsonPropertyName("windSpeed")]
        public string? WindSpeed { get; set; }

        [JsonPropertyName("windGust")]
        public QuantitativeValue? WindGust { get; set; }

        [JsonPropertyName("windDirection")]
        public string? WindDirection { get; set; } // [ N, NNE, NE, ENE, E, ESE, SE, SSE, S, SSW, SW, WSW, W, WNW, NW, NNW ]

        [JsonPropertyName("icon")]
        public string IconUrl { get; set; } = string.Empty; // this is a URL string, NOT the actual icon itself

        [JsonPropertyName("shortForecast")]
        public string? ShortForecast { get; set; }

        [JsonPropertyName("detailedForecast")]
        public string? DetailedForecast { get; set; }
    }
}
