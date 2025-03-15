using MobWx.API.Common;
using MobWx.Lib.Models;

namespace MobWx.Tests.Api;

public class NwsEndpointPathsTests
{
    [Theory]
    [InlineData(40.7128, -74.0060, 12, "/alerts/active?point=40.7128,-74.006&limit=12")]
    [InlineData(34.0522, -118.2437, 20, "/alerts/active?point=34.0522,-118.2437&limit=20")]
    [InlineData(34.0522, -118.2437, null, "/alerts/active?point=34.0522,-118.2437&limit=12")]
    [InlineData(34.0522, -118.2437, 30, "/alerts/active?point=34.0522,-118.2437&limit=12")] // limit out of range
    public void GetActiveAlertPath_WithLimit_ShouldReturnExpectedPath(decimal latitude, decimal longitude, int? limit, string expected)
    {
        // Arrange
        var position = Position.Create(Coordinate.Create(latitude, longitude));
        Assert.NotNull(position);
        Assert.Equal(typeof(Position), position.GetType());

        // Act
        var result = NwsEndpointPaths.GetActiveAlertPath((Position)position, limit);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(40.7128, -74.0060, "/points/40.7128,-74.006")]
    [InlineData(34.0522, -118.2437, "/points/34.0522,-118.2437")]
    public void PointPath_ShouldReturnExpectedPath(decimal latitude, decimal longitude, string expected)
    {
        // Arrange
        var position = Position.Create(Coordinate.Create(latitude, longitude));
        Assert.NotNull(position);
        Assert.Equal(typeof(Position), position.GetType());

        // Act
        var result = NwsEndpointPaths.PointPath(position);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(40.7128, -74.0060, "/alerts/active?point=40.7128,-74.006&limit=12")]
    [InlineData(34.0522, -118.2437, "/alerts/active?point=34.0522,-118.2437&limit=12")]
    public void GetActiveAlertPath_ShouldReturnExpectedPath(decimal latitude, decimal longitude, string expected)
    {
        // Arrange
        var position = Position.Create(Coordinate.Create(latitude, longitude));
        Assert.NotNull(position);
        Assert.Equal(typeof(Position), position.GetType());

        // Act
        var result = NwsEndpointPaths.GetActiveAlertPath(position);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("stationId", "/stations/stationId/observations/latest")]
    [InlineData("testStation", "/stations/testStation/observations/latest")]
    public void LatestObsPath_ShouldReturnExpectedPath(string stationId, string expected)
    {
        // Act
        var result = NwsEndpointPaths.LatestObsPath(stationId);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void PointPath_ShouldReturnEmptyString_WhenPositionHasCoordinateWithNulls()
    {
        // Arrange
        var expected = string.Empty;
        var position = Position.Create(new Coordinate()); // this is the anti-pattern for this project

        // Act
        var result = NwsEndpointPaths.PointPath(position);

        // Assert
        Assert.Equal(expected, result);
    }
}
