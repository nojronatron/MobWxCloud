using MobWx.Lib.Models;
using MobWx.Lib.Helpers;

namespace MobWx.API.Models;

public partial class CurrentObservation
{
    public static CurrentObservation Create(Observation observation)
    {
        int? temperatureC = observation.Temperature?.ToNullableInt();
        int? temperatureF = temperatureC is not null
            ? UnitConverter.ToFarenheit(temperatureC.Value)
            : null;

        int? dewpointC = observation.Dewpoint?.ToNullableInt();
        int? dewpointF = dewpointC is not null
            ? UnitConverter.ToFarenheit(dewpointC.Value)
            : null;

        int? windDirection = observation.WindDirection?.ToNullableInt();

        int? windSpeedKph = observation.WindSpeed?.ToNullableInt();
        int? windSpeedMph = windSpeedKph is not null
            ? UnitConverter.ToMilesPerHour(windSpeedKph.Value)
            : null;

        int? windGustKph = observation.WindGust?.ToNullableInt();
        int? windGustMph = windGustKph is not null
            ? UnitConverter.ToMilesPerHour(windGustKph.Value)
            : null;

        int? pressureHpa = observation.BarometricPressureHpa?.ToNullableInt();
        double? pressureMb = pressureHpa is not null
            ? UnitConverter.ToMillibars(pressureHpa.Value)
            : null;
        double? pressureIn = pressureHpa is not null
            ? UnitConverter.ToInchesMercury(pressureHpa.Value)
            : null;

        int? visibilityM = observation.Visibility?.ToNullableInt();
        int? visibilityMi = visibilityM is not null
            ? UnitConverter.ToVisibleMiles(visibilityM.Value)
            : null;

        int? maxTemperatureC = observation.MaxTemperatureLast24Hours?.ToNullableInt();
        int? maxTemperatureF = maxTemperatureC is not null
            ? UnitConverter.ToFarenheit(maxTemperatureC.Value)
            : null;
        int? minTemperatureC = observation.MinTemperatureLast24Hours?.ToNullableInt();
        int? minTemperatureF = minTemperatureC is not null
            ? UnitConverter.ToFarenheit(minTemperatureC.Value)
            : null;

        int? precipitationMmHr = observation.PrecipitationLastHour?.ToNullableInt();
        double? precipitationInchesHour = precipitationMmHr is not null
            ? UnitConverter.ToInchesPrecip(precipitationMmHr.Value)
            : null;

        int? windChillC = observation.WindChill?.ToNullableInt();
        int? windChillF = windChillC is not null
            ? UnitConverter.ToFarenheit(windChillC.Value)
            : null;

        int? heatIndexC = observation.HeatIndex?.ToNullableInt();
        int? heatIndexF = heatIndexC is not null
            ? UnitConverter.ToFarenheit(heatIndexC.Value)
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
