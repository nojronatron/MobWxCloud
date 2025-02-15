using MobWx.Lib.Models;
using MobWx.Lib.Helpers;
using MobWx.Lib.Enums;
using MobWx.Lib.Models.Nws;

namespace MobWx.Tests.Api;

public class CurrentObservationTests
{
    [Fact]
    public void Create_ShouldMapObservationToCurrentObservation()
    {
        // Arrange
        var observation = new Observation
        {
            Id = "TestId",
            Geometry = "Point(-123.45, 45.678)",
            StationElevationM = new QuantitativeValue { Value=100 },
            Station = "TestStation",
            Timestamp = new DateTime(2025, 1, 31, 3, 53, 0),
            RawMessage = "Test raw message",
            TextDescription = "Cloudy",
            Icon = "http://example.com/icon.png",
            TemperatureC = new QuantitativeValue { Value = 7.8, UnitCode = "wmoUnit:degC" },
            DewpointC = new QuantitativeValue { Value = -5, UnitCode = "wmoUnit:degC" },
            WindDirection = new QuantitativeValue { Value = 170, UnitCode = "wmoUnit:degree_(angle)" },
            WindSpeedKph = new QuantitativeValue { Value = 18.36, UnitCode = "wmoUnit:km_h-1" },
            WindGustKph = new QuantitativeValue { Value = null, UnitCode = "wmoUnit:km_h-1" },
            BarometricPressurePa = new QuantitativeValue { Value = 101860, UnitCode = "wmoUnit:Pa" },
            VisibilityM = new QuantitativeValue { Value = 16090, UnitCode = "wmoUnit:m" },
            MaxTempCLast24Hours = new QuantitativeValue { Value = null, UnitCode = "wmoUnit:degC" },
            MinTempCLast24Hours = new QuantitativeValue { Value = null, UnitCode = "wmoUnit:degC" },
            PrecipitationLastHourMm = new QuantitativeValue { Value = null, UnitCode = "wmoUnit:mm" },
            RhPercent = new QuantitativeValue { Value = 80, UnitCode = "wmoUnit:percent" },
            WindChillC = new QuantitativeValue { Value = 4.8, UnitCode = "wmoUnit:degC" },
            HeatIndexC = new QuantitativeValue { Value = null, UnitCode = "wmoUnit:degC" },

            CloudLayers =
            [
                new CloudLayer
                {
                    CloudBaseM = new QuantitativeValue { Value = 2740, UnitCode = "wmoUnit:m" },
                    Amount = Amount.OVC
                }
            ]
        };

        // Act
        var currentObservation = CurrentObservation.Create(observation);

        // Assert
        Assert.NotNull(currentObservation);
        Assert.Equal("TestStation", currentObservation.StationUri);
        Assert.Equal(new DateTime(2025, 1, 31, 3, 53, 0), currentObservation.Timestamp);
        Assert.Equal("Test raw message", currentObservation.RawMessage);
        Assert.Equal("Cloudy", currentObservation.Description);
        Assert.Equal("http://example.com/icon.png", currentObservation.IconUrl);
        Assert.Equal(8, currentObservation.TemperatureC);
        Assert.Equal(46, currentObservation.TemperatureF);
        Assert.Equal(-5, currentObservation.DewpointC);
        Assert.Equal(23, currentObservation.DewpointF);
        Assert.Equal(170, currentObservation.WindDirection);
        Assert.Equal(18, currentObservation.WindSpeedKph);
        Assert.Equal(11, currentObservation.WindSpeedMph);
        Assert.Null(currentObservation.WindGustKph);
        Assert.Null(currentObservation.WindGustMph);
        Assert.NotNull(currentObservation.PressureMb);
        Assert.Equal(1018.6, currentObservation.PressureMb);
        Assert.NotNull(currentObservation.PressureIn);
        Assert.Equal(30.08, currentObservation.PressureIn);
        Assert.Equal(16090, currentObservation.VisibilityMeters);
        Assert.Equal(10, currentObservation.VisibilityMiles);
        Assert.Null(currentObservation.MaxTemperatureC);
        Assert.Null(currentObservation.MaxTemperatureF);
        Assert.Null(currentObservation.MinTemperatureC);
        Assert.Null(currentObservation.MinTemperatureF);
        Assert.Null(currentObservation.PrecipitationMmHr);
        Assert.Null(currentObservation.PrecipitationInchHr);
        Assert.Equal(5, currentObservation.WindChillC);
        Assert.Equal(41, currentObservation.WindChillF);
        Assert.Null(currentObservation.HeatIndexC);
        Assert.Null(currentObservation.HeatIndexF);
        Assert.NotNull(currentObservation.CloudLayers);
        Assert.Single(currentObservation.CloudLayers);
        Assert.NotNull(currentObservation.CloudLayers[0]);
        Assert.NotNull(currentObservation.CloudLayers[0].Description);
        Assert.Equal("OVC", currentObservation.CloudLayers[0].Description);
        Assert.NotNull(currentObservation.CloudLayers[0].HeightMeters);
        Assert.Equal("2740", currentObservation.CloudLayers[0].HeightMeters);
        Assert.Equal("8990", currentObservation.CloudLayers[0].HeightFeet);
    }

    [Theory]
    [InlineData(0, 32)]
    [InlineData(100, 212)]
    [InlineData(-40, -40)]
    public void ToFarenheit_ShouldConvertCelsiusToFarenheit(double celsius, int expectedFarenheit)
    {
        // Act
        var result = UnitConverter.ToFarenheit(celsius);

        // Assert
        Assert.Equal(expectedFarenheit, result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1000, 1)] // 0.621371 miles
    [InlineData(3000, 2)] // 1.864114 miles
    [InlineData(5000, 3)] // 3.106855 miles
    [InlineData(10000, 6)] // 6.213712 miles
    [InlineData(14484, 9)]
    [InlineData(16090, 10)] // 16093.4m ~ 10mi
    public void ToMiles_ShouldConvertKilometersToMiles(int kilometers, int expectedMiles)
    {
        // Act
        var result = UnitConverter.ToMiles(kilometers);

        // Assert
        Assert.Equal(expectedMiles, result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(100, 62)]
    [InlineData(50, 31)]
    public void ToMilesPerHour_ShouldConvertKilometersPerHourToMilesPerHour(int kilometersPerHour, int expectedMilesPerHour)
    {
        // Act
        var result = UnitConverter.ToMilesPerHour(kilometersPerHour);

        // Assert
        Assert.Equal(expectedMilesPerHour, result);
    }

    [Theory]
    [InlineData(87000, 25.69)]
    [InlineData(99000, 29.23)]
    [InlineData(100680, 29.73)]
    [InlineData(100730, 29.75)]
    [InlineData(102000, 30.12)]
    [InlineData(108480, 32.03)]
    public void ToInchesMercury_ShouldConvertPascalsToInchesMercury(int pascals, double expectedInchesMercury)
    {
        // Act
        var result = UnitConverter.ToInchesMercury(pascals);

        // Assert
        Assert.Equal(expectedInchesMercury, result); // rounding is done by the method under test
    }

    [Theory]
    [InlineData(87000, 870.0)]
    [InlineData(99000, 990.0)]
    [InlineData(100680, 1006.8)]
    [InlineData(100730, 1007.3)]
    [InlineData(102000, 1020.0)]
    [InlineData(108480, 1084.8)]
    public void ToMillibars_ShouldConvertPascalsToMillibars(int pascals, double expectedMillibars)
    {
        // Act
        var result = UnitConverter.ToMillibars(pascals);

        // Assert
        Assert.Equal(expectedMillibars, result); // rounding is done by the method under test
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(25, 1)]
    [InlineData(100, 4)]
    public void ToInches_ShouldConvertMillimetersToInches(int millimeters, int expectedInches)
    {
        // Act
        var result = UnitConverter.ToInches(millimeters);

        // Assert
        Assert.Equal(expectedInches, result);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 3)]
    [InlineData(10, 33)]
    public void ToFeet_ShouldConvertMetersToFeet(int meters, int expectedFeet)
    {
        // Act
        var result = UnitConverter.ToFeet(meters);

        // Assert
        Assert.Equal(expectedFeet, result);
    }
}
