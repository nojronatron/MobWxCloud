using MobWx.Lib.Enums;
using MobWx.Lib.Models;

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
                PointLocation = "POINT(45,-123)",
                StationElevation = new Elevation { Value = 100, UnitCode = "wmoUnit:m" },
                Station = "TestStation",
                Timestamp = new DateTime(2025, 1, 31, 3, 53, 0),
                RawMessage = "Test raw message",
                TextDescription = "Cloudy",
                Icon = "http://example.com/icon.png",
                Temperature = new Temperature { Value = 7.8, UnitCode = "wmoUnit:degC" },
                Dewpoint = new Dewpoint { Value = -1.1, UnitCode = "wmoUnit:degC" },
                WindDirection = new WindDirection { Value = 170, UnitCode = "wmoUnit:degree_(angle)" },
                WindSpeed = new WindSpeed { Value = 18.36, UnitCode = "wmoUnit:km_h-1" },
                WindGust = new WindGust { Value = null, UnitCode = "wmoUnit:km_h-1" },
                BarometricPressureHpa = new BarometricPressure { Value = 101860, UnitCode = "wmoUnit:Pa" },
                Visibility = new Visibility { Value = 16090, UnitCode = "wmoUnit:m" },
                MaxTemperatureLast24Hours = new MaxTemperatureLast24Hours { Value = null, UnitCode = "wmoUnit:degC" },
                MinTemperatureLast24Hours = new MinTemperatureLast24Hours { Value = null, UnitCode = "wmoUnit:degC" },
                PrecipitationLastHour = new PrecipitationLastHour { Value = null, UnitCode = "wmoUnit:mm" },
                RelativeHumidity = new Rh { Value = 93, UnitCode = "wmoUnit:percent" },
                WindChill = new WindChill { Value = 4.8, UnitCode = "wmoUnit:degC" },
                HeatIndex = new HeatIndex { Value = null, UnitCode = "wmoUnit:degC" },
                CloudLayers = new List<CloudLayer>
                {
                    new CloudLayer
                    {
                        CloudBase = new MeasurementInt { Value = 2740, UnitCode = "wmoUnit:m" },
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
