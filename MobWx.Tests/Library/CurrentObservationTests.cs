using MobWx.Lib.Enums;
using MobWx.Lib.Helpers;
using MobWx.Lib.Models.Nws;
using MobWx.Lib.Models;

namespace MobWx.Tests.Library;

public class CurrentObservationTests
{
    [Fact]
    public void Create_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var observation = new Observation
        {
            TemperatureC = new QuantitativeValue { Value = 20m },
            DewpointC = new QuantitativeValue { Value = 10m },
            WindDirection = new QuantitativeValue { Value = 180m },
            WindSpeedKph = new QuantitativeValue { Value = 30m },
            WindGustKph = new QuantitativeValue { Value = 50m },
            BarometricPressurePa = new QuantitativeValue { Value = 101325m },
            VisibilityM = new QuantitativeValue { Value = 10000m },
            MaxTempCLast24Hours = new QuantitativeValue { Value = 25m },
            MinTempCLast24Hours = new QuantitativeValue { Value = 15m },
            PrecipitationLastHourMm = new QuantitativeValue { Value = 5m },
            WindChillC = new QuantitativeValue { Value = -5m },
            HeatIndexC = new QuantitativeValue { Value = 30m },
            RhPercent = new QuantitativeValue { Value = 50m },
            CloudLayers = new List<CloudLayer>
                {
                    new CloudLayer
                    {
                        Amount = Amount.BKN,
                        CloudBaseM = new QuantitativeValue { Value = 2000m }
                    }
                },
            Geometry = "POINT (-74.0060 40.7128)",
            StationElevationM = new QuantitativeValue { Value = 10m },
            Station = "KJFK",
            Timestamp = DateTime.UtcNow,
            RawMessage = "METAR KJFK 121651Z 18015G25KT 10SM BKN020 20/10 A2992 RMK AO2",
            TextDescription = "Partly Cloudy",
            Icon = "https://api.weather.gov/icons/land/day/bkn?size=medium"
        };

        // Constants for expected values
        const int expectedTemperatureF = 68; // 20°C to °F
        const int expectedDewpointF = 50; // 10°C to °F
        const int expectedWindSpeedMph = 19; // 30 kph to mph
        const int expectedWindGustMph = 31; // 50 kph to mph
        const decimal expectedPressureMb = 1013.2m; // 101325 Pa to mb
        const decimal expectedPressureIn = 29.92m; // 101325 Pa to inHg
        const int expectedVisibilityMi = 6; // 10000 m to miles
        const int expectedMaxTemperatureF = 77; // 25°C to °F
        const int expectedMinTemperatureF = 59; // 15°C to °F
        const decimal expectedPrecipitationInchHr = 0.2m; // 5 mm to inches
        const int expectedWindChillF = 23; // -5°C to °F
        const int expectedHeatIndexF = 86; // 30°C to °F

        // Act
        var currentObservation = CurrentObservation.Create(observation);

        // Assert
        Assert.Equal(observation.Geometry, currentObservation.StationLocation);
        Assert.Equal(observation.StationElevationM?.Value, currentObservation.StationElevation);
        Assert.Equal(observation.Station, currentObservation.StationUri);
        Assert.Equal(observation.Timestamp, currentObservation.Timestamp);
        Assert.Equal(observation.RawMessage, currentObservation.RawMessage);
        Assert.Equal(observation.TextDescription, currentObservation.Description);
        Assert.Equal(observation.Icon, currentObservation.IconUrl);
        Assert.Equal(observation.TemperatureC?.Value, currentObservation.TemperatureC);
        Assert.Equal(expectedTemperatureF, currentObservation.TemperatureF);
        Assert.Equal(observation.DewpointC?.Value, currentObservation.DewpointC);
        Assert.Equal(expectedDewpointF, currentObservation.DewpointF);
        Assert.Equal(observation.WindDirection?.Value, currentObservation.WindDirection);
        Assert.Equal(observation.WindSpeedKph?.Value, currentObservation.WindSpeedKph);
        Assert.Equal(expectedWindSpeedMph, currentObservation.WindSpeedMph);
        Assert.Equal(observation.WindGustKph?.Value, currentObservation.WindGustKph);
        Assert.Equal(expectedWindGustMph, currentObservation.WindGustMph);
        Assert.Equal(expectedPressureMb, currentObservation.PressureMb);
        Assert.Equal(expectedPressureIn, currentObservation.PressureIn);
        Assert.Equal(observation.VisibilityM?.Value, currentObservation.VisibilityMeters);
        Assert.Equal(expectedVisibilityMi, currentObservation.VisibilityMiles);
        Assert.Equal(observation.MaxTempCLast24Hours?.Value, currentObservation.MaxTemperatureC);
        Assert.Equal(expectedMaxTemperatureF, currentObservation.MaxTemperatureF);
        Assert.Equal(observation.MinTempCLast24Hours?.Value, currentObservation.MinTemperatureC);
        Assert.Equal(expectedMinTemperatureF, currentObservation.MinTemperatureF);
        Assert.Equal(observation.PrecipitationLastHourMm?.Value, currentObservation.PrecipitationMmHr);
        Assert.Equal(expectedPrecipitationInchHr, currentObservation.PrecipitationInchHr);
        Assert.Equal(observation.RhPercent?.Value, currentObservation.RelativeHumidity);
        Assert.Equal(observation.WindChillC?.Value, currentObservation.WindChillC);
        Assert.Equal(expectedWindChillF, currentObservation.WindChillF);
        Assert.Equal(observation.HeatIndexC?.Value, currentObservation.HeatIndexC);
        Assert.Equal(expectedHeatIndexF, currentObservation.HeatIndexF);
        Assert.NotNull(currentObservation.CloudLayers);
        Assert.Single(currentObservation.CloudLayers);
        Assert.Equal(observation.CloudLayers[0].CloudBaseM?.Value.ToString(), currentObservation.CloudLayers[0].HeightMeters);
        Assert.Equal(observation.CloudLayers[0].Amount.ToString(), currentObservation.CloudLayers[0].Description);
    }
}
