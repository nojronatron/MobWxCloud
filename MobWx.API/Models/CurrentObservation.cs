using MobWx.Lib.Models;

namespace MobWx.API.Models;

public partial class CurrentObservation
{
    public string Location { get; set; } = string.Empty;
    public double StationElevation { get; set; } = 0.0d;
    public string Station { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string RawMessage { get; set; } = string.Empty; // could be METAR
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public double? TemperatureC { get; set; }
    public int? TemperatureF { get; set; }
    public double? DewpointC { get; set; }
    public int? DewpointF { get; set; }
    public int? WindDirection { get; set; }
    public double? WindSpeedKph { get; set; }
    public int? WindSpeedMph { get; set; }
    public double? WindGustKph { get; set; }
    public int? WindGustMph { get; set; }
    public int? PressureMb { get; set; } // whole millimeters
    public double? PressureIn { get; set; } // proportianal inches to the tenths
    public int? VisibilityMeters { get; set; }
    public int? VisibilityMiles { get; set; }
    public double? MaxTemperatureC { get; set; } // 24hr period
    public int? MaxTemperatureF { get; set; } // 24hr period
    public double? MinTemperatureC { get; set; } // 24hr period
    public int? MinTemperatureF { get; set; } // 24hr period
    public int? PrecipitationMmHr { get; set; } // 1hr period
    public double? PrecipitationInchHr { get; set; } // 1hr period
    public double? WindChillC { get; set; }
    public int? WindChillF { get; set; }
    public double? HeatIndexC { get; set; }
    public int? HeatIndexF { get; set; }
    public List<CloudLayer>? CloudLayers { get; set; }

    public static CurrentObservation Create(Observation observation)
    {
        int? temperatureC = observation.Temperature?.ToNullableInt();
        int? temperatureF = temperatureC is not null
            ? CurrentObservation.ToFarenheit(temperatureC.Value)
            : null;

        int? dewpointC = observation.Dewpoint?.ToNullableInt();
        int? dewpointF = dewpointC is not null
            ? CurrentObservation.ToFarenheit(dewpointC.Value)
            : null;

        int? windDirection = observation.WindDirection?.ToNullableInt();

        int? windSpeedKph = observation.WindSpeed?.ToNullableInt();
        int? windSpeedMph = windSpeedKph is not null
            ? CurrentObservation.ToMilesPerHour(windSpeedKph.Value)
            : null;

        int? windGustKph = observation.WindGust?.ToNullableInt();
        int? windGustMph = windGustKph is not null
            ? CurrentObservation.ToMilesPerHour(windGustKph.Value)
            : null;

        int? pressureMb = observation.BarometricPressureMb?.ToNullableInt();
        double? pressureIn = pressureMb is not null
            ? CurrentObservation.ToInchesMercury((int)pressureMb)
            : null;

        int? visibilityM = observation.Visibility?.ToNullableInt();
        int? visibilityMi = visibilityM is not null
            ? CurrentObservation.ToVisibleMiles(visibilityM.Value)
            : null;

        int? maxTemperatureC = observation.MaxTemperatureLast24Hours?.ToNullableInt();
        int? maxTemperatureF = maxTemperatureC is not null
            ? CurrentObservation.ToFarenheit(maxTemperatureC.Value)
            : null;
        int? minTemperatureC = observation.MinTemperatureLast24Hours?.ToNullableInt();
        int? minTemperatureF = minTemperatureC is not null
            ? CurrentObservation.ToFarenheit(minTemperatureC.Value)
            : null;

        int? precipitationMmHr = observation.PrecipitationLastHour?.ToNullableInt();
        double? precipitationInchesHour = precipitationMmHr is not null
            ? CurrentObservation.ToInchesPrecip(precipitationMmHr.Value)
            : null;

        int? windChillC = observation.WindChill?.ToNullableInt();
        int? windChillF = windChillC is not null
            ? CurrentObservation.ToFarenheit(windChillC.Value)
            : null;

        int? heatIndexC = observation.HeatIndex?.ToNullableInt();
        int? heatIndexF = heatIndexC is not null
            ? CurrentObservation.ToFarenheit(heatIndexC.Value)
            : null;

        return new CurrentObservation
        {
            Station = observation.Station ?? string.Empty,
            Timestamp = observation.Timestamp ?? DateTime.MinValue,
            RawMessage = observation.RawMessage ?? string.Empty,
            Description = observation.TextDescription ?? string.Empty,
            IconUrl = observation.Icon ?? string.Empty,
            TemperatureC = temperatureC,
            TemperatureF = temperatureF,
            DewpointC = dewpointC,
            DewpointF = dewpointF,
            WindDirection = windDirection,
            WindSpeedKph = windSpeedKph,
            WindSpeedMph = windSpeedMph,
            WindGustKph = windGustKph,
            WindGustMph = windGustMph,
            PressureIn = pressureIn,
            PressureMb = pressureMb,
            VisibilityMeters = visibilityM,
            VisibilityMiles = visibilityMi,
            MaxTemperatureC = maxTemperatureC,
            MaxTemperatureF = maxTemperatureF,
            MinTemperatureC = minTemperatureC,
            MinTemperatureF = minTemperatureF,
            PrecipitationMmHr = precipitationMmHr,
            PrecipitationInchHr = precipitationInchesHour,
            WindChillC = windChillC,
            WindChillF = windChillF,
            HeatIndexC = heatIndexC,
            HeatIndexF = heatIndexF,
            CloudLayers = observation.CloudLayers
        };
    }
}
