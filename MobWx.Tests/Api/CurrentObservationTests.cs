using MobWx.Lib.Models;
using MobWx.API.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace MobWx.Tests.Api;

public class CurrentObservationTests
{
    [Fact]
    public void Create_ShouldMapObservationToCurrentObservation()
    {
        // Arrange
        var observation = new Observation
        {
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
        var currentObservation = CurrentObservation.Create(observation);

        // Assert
        Assert.NotNull(currentObservation);
        Assert.Equal("TestStation", currentObservation.Station);
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
        Assert.Equal(101860, currentObservation.PressureMb);
        Assert.Equal(3007.92, Math.Round((double)currentObservation.PressureIn, 2));
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
        Assert.NotNull(currentObservation.CloudLayers[0].Base);
        Assert.Equal(2740, currentObservation.CloudLayers[0].Base.Value);
        Assert.Equal(Amount.OVC, currentObservation.CloudLayers[0].Amount);
    }

    [Theory]
    [InlineData(0, 32)]
    [InlineData(100, 212)]
    [InlineData(-40, -40)]
    public void ToFarenheit_ShouldConvertCelsiusToFarenheit(double celsius, int expectedFarenheit)
    {
        // Act
        var result = CurrentObservation.ToFarenheit(celsius);

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
        var result = CurrentObservation.ToVisibleMiles(kilometers);

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
        var result = CurrentObservation.ToMilesPerHour(kilometersPerHour);

        // Assert
        Assert.Equal(expectedMilesPerHour, result);
    }

    [Theory]
    [InlineData(1013.25, 29.92)]
    [InlineData(1000, 29.53)]
    public void ToInchesMercury_ShouldConvertMillibarsToInchesMercury(double millibars, double expectedInchesMercury)
    {
        // Act
        var result = CurrentObservation.ToInchesMercury(millibars);

        // Assert
        Assert.Equal(expectedInchesMercury, result, 2);
    }

    [Theory]
    [InlineData(29.92, 1013.3)]
    [InlineData(29.53, 1000)]
    public void ToMillibars_ShouldConvertInchesMercuryToMillibars(double inchesMercury, double expectedMillibars)
    {
        // Act
        var result = CurrentObservation.ToMillibars(inchesMercury);

        // Assert
        //Assert.Equal(expectedMillibars, result, 1);
        Assert.InRange(expectedMillibars, result - 0.5, result + 0.5);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(25, 1)]
    [InlineData(100, 4)]
    public void ToInches_ShouldConvertMillimetersToInches(int millimeters, int expectedInches)
    {
        // Act
        var result = CurrentObservation.ToInchesPrecip(millimeters);

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
        var result = CurrentObservation.ToFeet(meters);

        // Assert
        Assert.Equal(expectedFeet, result);
    }
}
