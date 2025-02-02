using MobWx.Lib.Models;

namespace MobWx.API.Models;

public partial class CurrentObservation
{
    public string Station { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string RawMessage { get; set; } = string.Empty; // could be METAR
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public int? TemperatureC { get; set; }
    public int? TemperatureF { get; set; }
    public int? DewpointC { get; set; }
    public int? DewpointF { get; set; }
    public int? WindDirection { get; set; }
    public int? WindSpeedKph { get; set; }
    public int? WindSpeedMph { get; set; }
    public int? WindGustKph { get; set; }
    public int? WindGustMph { get; set; }
    public int? PressureMb { get; set; } // whole millimeters
    public double? PressureIn { get; set; } // proportianal inches to the tenths
    public int? VisibilityMeters { get; set; }
    public int? VisibilityMiles { get; set; }
    public int? MaxTemperatureC { get; set; } // 24hr period
    public int? MaxTemperatureF { get; set; } // 24hr period
    public int? MinTemperatureC { get; set; } // 24hr period
    public int? MinTemperatureF { get; set; } // 24hr period
    public int? PrecipitationMmHr { get; set; } // 1hr period
    public double? PrecipitationInchHr { get; set; } // 1hr period
    public int? WindChillC { get; set; }
    public int? WindChillF { get; set; }
    public int? HeatIndexC { get; set; }
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
            //TemperatureC = observation.Temperature?.ToInt(),
            TemperatureC = temperatureC,
            //TemperatureF = observation.Temperature is not null
            //    ? CurrentObservation.ToFarenheit(observation.Temperature.ToDouble())
            //    : null,
            TemperatureF = temperatureF,
            //DewpointC = observation.Dewpoint?.ToInt(),
            DewpointC = dewpointC,
            //DewpointF = observation.Dewpoint is not null
            //    ? CurrentObservation.ToFarenheit(observation.Dewpoint.ToDouble())
            //    : null,
            DewpointF = dewpointF,
            //WindDirection = observation.WindDirection?.ToInt(),
            WindDirection = windDirection,
            //WindSpeedKph = observation.WindSpeed?.ToInt(),
            WindSpeedKph = windSpeedKph,
            //WindSpeedMph = observation.WindSpeed is not null
            //    ? CurrentObservation.ToMilesPerHour(observation.WindSpeed.ToInt())
            //    : null,
            WindSpeedMph = windSpeedMph,
            //WindGustKph = observation.WindGust is not null
            //    ? observation.WindGust.Value is not null
            //        ? observation.WindGust.ToInt()
            //        : observation.WindGust.ToNullableInt()
            //    : null,
            WindGustKph = windGustKph,
            //WindGustMph = observation.WindGust is not null
            //    ? CurrentObservation.ToMilesPerHour(observation.WindGust.ToInt())
            //    : null,
            WindGustMph = windGustMph,
            //PressureIn = observation.BarometricPressure?.ToDouble(),
            PressureIn = pressureIn,
            //PressureMb = observation.BarometricPressure is not null
            //    ? CurrentObservation.ToMillibars(observation.BarometricPressure.ToDouble())
            //    : null,
            PressureMb = pressureMb,
            //VisibilityKm = observation.Visibility?.ToInt(),
            VisibilityMeters = visibilityM,
            //VisibilityMi = observation.Visibility is not null
            //    ? CurrentObservation.ToMiles(observation.Visibility.ToInt())
            //    : null,
            VisibilityMiles = visibilityMi,
            //MaxTemperatureC = observation.MaxTemperatureLast24Hours?.ToInt(),
            MaxTemperatureC = maxTemperatureC,
            //MaxTemperatureF = observation.MaxTemperatureLast24Hours is not null
            //    ? CurrentObservation.ToFarenheit(observation.MaxTemperatureLast24Hours.ToInt())
            //    : null,
            MaxTemperatureF = maxTemperatureF,
            //MinTemperatureC = observation.MinTemperatureLast24Hours?.ToInt(),
            MinTemperatureC = minTemperatureC,
            //MinTemperatureF = observation.MinTemperatureLast24Hours is not null
            //    ? CurrentObservation.ToFarenheit(observation.MinTemperatureLast24Hours.ToInt())
            //    : null,
            MinTemperatureF = minTemperatureF,
            //PrecipitationMmHr = observation.PrecipitationLastHour?.ToInt(),
            PrecipitationMmHr = precipitationMmHr,
            //PrecipitationInchHr = observation.PrecipitationLastHour is not null
            //    ? CurrentObservation.ToInches(observation.PrecipitationLastHour.ToInt())
            //    : null,
            PrecipitationInchHr = precipitationInchesHour,
            //WindChillC = observation.WindChill?.ToInt(),
            WindChillC = windChillC,
            //WindChillF = observation.WindChill is not null
            //    ? CurrentObservation.ToFarenheit(observation.WindChill.ToInt())
            //    : null,
            WindChillF = windChillF,
            //HeatIndexC = observation.HeatIndex?.ToInt(),
            HeatIndexC = heatIndexC,
            //HeatIndexF = observation.HeatIndex is not null
            //    ? CurrentObservation.ToFarenheit(observation.HeatIndex.ToInt())
            //    : null,
            HeatIndexF = heatIndexF,
            CloudLayers = observation.CloudLayers
        };
    }
}
