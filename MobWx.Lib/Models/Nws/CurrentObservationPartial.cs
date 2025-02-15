using MobWx.Lib.Helpers;
using MobWx.Lib.Models.QVModels;

namespace MobWx.Lib.Models.Nws;

public partial class CurrentObservation
{
    public static CurrentObservation Create(Observation observation)
    {
        int? temperatureC = observation.TemperatureC?.ToNullableInt();
        int? temperatureF = temperatureC is not null
            ? UnitConverter.ToFarenheit(temperatureC.Value)
            : null;

        int? dewpointC = observation.DewpointC?.ToNullableInt();
        int? dewpointF = dewpointC is not null
            ? UnitConverter.ToFarenheit(dewpointC.Value)
            : null;

        int? windDirection = observation.WindDirection?.ToNullableInt();

        int? windSpeedKph = observation.WindSpeedKph?.ToNullableInt();
        int? windSpeedMph = windSpeedKph is not null
            ? UnitConverter.ToMilesPerHour(windSpeedKph.Value)
            : null;

        int? windGustKph = observation.WindGustKph?.ToNullableInt();
        int? windGustMph = windGustKph is not null
            ? UnitConverter.ToMilesPerHour(windGustKph.Value)
            : null;

        int? pressureHpa = observation.BarometricPressurePa?.ToNullableInt();
        double? pressureMb = pressureHpa is not null
            ? UnitConverter.ToMillibars(pressureHpa.Value)
            : null;
        double? pressureIn = pressureHpa is not null
            ? UnitConverter.ToInchesMercury(pressureHpa.Value)
            : null;

        int? visibilityM = observation.VisibilityM?.ToNullableInt();
        int? visibilityMi = visibilityM is not null
            ? UnitConverter.ToMiles(visibilityM.Value)
            : null;

        int? maxTemperatureC = observation.MaxTempCLast24Hours?.ToNullableInt();
        int? maxTemperatureF = maxTemperatureC is not null
            ? UnitConverter.ToFarenheit(maxTemperatureC.Value)
            : null;
        int? minTemperatureC = observation.MinTempCLast24Hours?.ToNullableInt();
        int? minTemperatureF = minTemperatureC is not null
            ? UnitConverter.ToFarenheit(minTemperatureC.Value)
            : null;

        int? precipitationMmHr = observation.PrecipitationLastHourMm?.ToNullableInt();
        double? precipitationInchesHour = precipitationMmHr is not null
            ? UnitConverter.ToInches(precipitationMmHr.Value)
            : null;

        int? windChillC = observation.WindChillC?.ToNullableInt();
        int? windChillF = windChillC is not null
            ? UnitConverter.ToFarenheit(windChillC.Value)
            : null;

        int? heatIndexC = observation.HeatIndexC?.ToNullableInt();
        int? heatIndexF = heatIndexC is not null
            ? UnitConverter.ToFarenheit(heatIndexC.Value)
            : null;

        List<SimpleCloudLayer> SimpleCloudLayers = [];

        if (observation.CloudLayers is not null && observation.CloudLayers.Count > 0)
        {
            foreach (var cloudLayer in observation.CloudLayers)
            {
                if (cloudLayer.Amount is not null && cloudLayer.Amount.HasValue)
                {
                    string clAmount = cloudLayer.Amount.ToString() ?? string.Empty;

                    SimpleCloudLayers
                        .Add(new SimpleCloudLayer
                        {
                            HeightMeters = cloudLayer.CloudBaseM?.ToString() ?? null,
                            Description = clAmount
                        });
                }
            }
        }

        return new CurrentObservation
        {
            StationLocation = observation.Geometry,
            StationElevation = observation.StationElevationM?.ToNullableDouble(),
            StationUri = observation.Station ?? string.Empty,
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
            RelativeHumidity = observation.RhPercent?.ToNullableInt(),
            WindChillC = windChillC,
            WindChillF = windChillF,
            HeatIndexC = heatIndexC,
            HeatIndexF = heatIndexF,
            CloudLayers = SimpleCloudLayers
        };
    }
}
