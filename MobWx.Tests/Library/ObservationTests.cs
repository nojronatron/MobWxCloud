using MobWx.Lib.Enums;
using MobWx.Lib.Models;
using MobWx.Lib.Models.Nws;

namespace MobWx.Tests.Library
{
    public class ObservationTests
    {
        [Fact]
        public void Observation_ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var observation = new Observation
            {
                Id = "123",
                Geometry = "POINT(45,-123)",
                StationElevationM = new Elevation { Value = 100, UnitCode = "wmoUnit:m" },
                Station = "TestStation",
                Timestamp = new DateTime(2025, 1, 31, 3, 53, 0),
                RawMessage = "Test raw message",
                TextDescription = "Cloudy",
                Icon = "http://example.com/icon.png",
                TemperatureC = new QuantitativeValue { Value = 7.8, UnitCode = "wmoUnit:degC" },
                DewpointC = new QuantitativeValue { Value = -1.1, UnitCode = "wmoUnit:degC" },
                WindDirection = new QuantitativeValue { Value = 170, UnitCode = "wmoUnit:degree_(angle)" },
                WindSpeedKph = new QuantitativeValue { Value = 18.36, UnitCode = "wmoUnit:km_h-1" },
                WindGustKph = new QuantitativeValue { Value = null, UnitCode = "wmoUnit:km_h-1" },
                BarometricPressurePa = new QuantitativeValue { Value = 101860, UnitCode = "wmoUnit:Pa" },
                VisibilityM = new QuantitativeValue { Value = 16090, UnitCode = "wmoUnit:m" },
                MaxTempCLast24Hours = new QuantitativeValue { Value = null, UnitCode = "wmoUnit:degC" },
                MinTempCLast24Hours = new QuantitativeValue { Value = null, UnitCode = "wmoUnit:degC" },
                PrecipitationLastHourMm = new QuantitativeValue { Value = null, UnitCode = "wmoUnit:mm" },
                RhPercent = new QuantitativeValue { Value = 93, UnitCode = "wmoUnit:percent" },
                WindChillC = new QuantitativeValue { Value = 4.8, UnitCode = "wmoUnit:degC" },
                HeatIndexC = new QuantitativeValue { Value = null, UnitCode = "wmoUnit:degC" },
                CloudLayers = new List<CloudLayer>
                {
                    new CloudLayer
                    {
                        CloudBaseM = new QuantitativeValue { Value = 2740, UnitCode = "wmoUnit:m" },
                        Amount = Amount.OVC
                    }
                }
            };

            // Act
            var result = observation.ToString();

            // Assert
            var expected = @"***** Observation *****
Id: 123
Station: TestStation
Timestamp: 31-Jan-25 3:53:00 AM
Raw Message: Test raw message
Text Description: Cloudy
Icon: http://example.com/icon.png
Temperature: 7.8
Dewpoint: -1.1
Wind Direction: 170
Wind Speed: 18.36
Wind Gust: 
Barometric Pressure: 101860
Visibility: 16090
Max Temp Last 24 Hours: 
Min Temp Last 24 Hours: 
Precipitation Last Hour: 
Rh: 93
Wind Chill: 4.8
Heat Index: 
Cloud Layers:
  OVC, 2740
***** End Observation *****
".Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
            Assert.Equal(expected, result);
        }
    }
}
