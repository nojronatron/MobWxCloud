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
                Station = "TestStation",
                Timestamp = new DateTime(2025, 1, 31, 3, 53, 0),
                RawMessage = "Test raw message",
                TextDescription = "Cloudy",
                Icon = "http://example.com/icon.png",
                Temperature = new Measurement { Value = 7.8, UnitCode = "wmoUnit:degC" },
                Dewpoint = new Measurement { Value = -5, UnitCode = "wmoUnit:degC" },
                WindDirection = new Measurement { Value = 170, UnitCode = "wmoUnit:degree_(angle)" },
                WindSpeed = new Measurement { Value = 18.36, UnitCode = "wmoUnit:km_h-1" },
                WindGust = new Measurement { Value = null, UnitCode = "wmoUnit:km_h-1" },
                BarometricPressureMb = new Measurement { Value = 101860, UnitCode = "wmoUnit:Pa" },
                Visibility = new Measurement { Value = 16090, UnitCode = "wmoUnit:m" },
                MaxTemperatureLast24Hours = new Measurement { Value = null, UnitCode = "wmoUnit:degC" },
                MinTemperatureLast24Hours = new Measurement { Value = null, UnitCode = "wmoUnit:degC" },
                PrecipitationLastHour = new Measurement { Value = null, UnitCode = "wmoUnit:mm" },
                WindChill = new Measurement { Value = 4.8, UnitCode = "wmoUnit:degC" },
                HeatIndex = new Measurement { Value = null, UnitCode = "wmoUnit:degC" },
                CloudLayers = new List<CloudLayer>
                {
                    new CloudLayer
                    {
                        Base = new Measurement { Value = 2740, UnitCode = "wmoUnit:m" },
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
Dewpoint: -5
Wind Direction: 170
Wind Speed: 18.36
Wind Gust: 
Barometric Pressure: 101860
Visibility: 16090
Max Temp Last 24 Hours: 
Min Temp Last 24 Hours: 
Precipitation Last Hour: 
Wind Chill: 4.8
Heat Index: 
***** End Observation *****
".Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Measurement_ToString_ShouldReturnValue()
        {
            // Arrange
            var measurement = new Measurement { Value = 7.8, UnitCode = "wmoUnit:degC" };

            // Act
            var result = measurement.ToString();

            // Assert
            Assert.Equal("7.8", result);
        }

        [Fact]
        public void Measurement_MinValue_ShouldReturnMinValue()
        {
            // Act
            var result = Measurement.MinValue;

            // Assert
            Assert.Equal(double.MinValue, result.Value);
        }

        [Fact]
        public void Measurement_ZeroValue_ShouldReturnZeroValue()
        {
            // Act
            var result = Measurement.ZeroValue;

            // Assert
            Assert.Equal(0, result.Value);
        }

        [Fact]
        public void CloudLayer_DefaultValues_ShouldReturnExpectedValues()
        {
            // Arrange
            var cloudLayer = new CloudLayer();

            // Act & Assert
            Assert.Equal(Measurement.ZeroValue, cloudLayer.Base);
            // ovc is first item in enum therefore default
            Assert.Equal(Amount.OVC, cloudLayer.Amount);
        }
    }
}
