using MobWx.API.Common;
using MobWx.Lib.Models;
using MobWx.Lib.Models.Base;

namespace MobWx.Tests.Api;

public class NwsEndpointPathsTests
{
    [Theory]
    [InlineData("40.7128", "-74.0060", 12, "/points/40.7128,-74.0060?limit=12")]
    [InlineData("34.0522", "-118.2437", 20, "/points/34.0522,-118.2437?limit=20")]
    [InlineData("34.0522", "-118.2437", null, "/points/34.0522,-118.2437?limit=12")]
    [InlineData("34.0522", "-118.2437", 0, "/points/34.0522,-118.2437?limit=12")]
    [InlineData("40.7128", "-74.0060", 1, "/points/40.7128,-74.0060?limit=1")]
    [InlineData("34.0522", "-118.2437", 25, "/points/34.0522,-118.2437?limit=25")]
    [InlineData("34.0522", "-118.2437", 26, "/points/34.0522,-118.2437?limit=12")]
    [InlineData("34.0522", "-118.2437", 30, "/points/34.0522,-118.2437?limit=12")] // limit out of range
    public void PointPath_WithLimit_ShouldReturnExpectedPath(string latitude, string longitude, int? limit, string expected)
    {
        // Arrange
        var position = Position.Create(latitude, longitude);

        // Act
        var result = NwsEndpointPaths.PointPath(position, limit);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("40.7128", "-74.0060", "/alerts/active?point=40.7128,-74.0060&limit=12")]
    [InlineData("34.0522", "-118.2437", "/alerts/active?point=34.0522,-118.2437&limit=12")]
    public void GetActiveAlertPath_ShouldReturnExpectedPath(string latitude, string longitude, string expected)
    {
        // Arrange
        var position = new Position(latitude, longitude);

        // Act
        var result = NwsEndpointPaths.GetActiveAlertPath(position);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("gridId", "10", "20", "/gridpoints/GRIDID/10,20/stations")]
    [InlineData("test", "5", "15", "/gridpoints/TEST/5,15/stations")]
    public void FoRelativePath_ShouldReturnExpectedPath(string gridId, string gridX, string gridY, string expected)
    {
        // Act
        var result = NwsEndpointPaths.FoRelativePath(gridId, gridX, gridY);

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

    [Theory]
    [InlineData("40.7128", "-74.0060", "/points/40.7128,-74.0060?limit=12")]
    [InlineData("34.0522", "-118.2437", "/points/34.0522,-118.2437?limit=12")]
    public void PointPath_ShouldReturnExpectedPath(string latitude, string longitude, string expected)
    {
        // Arrange
        var position = PositionBase.Create(latitude, longitude);

        // Act
        var result = NwsEndpointPaths.PointPath(position);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void PointPath_ShouldReturnEmptyString_WhenPositionIsNullPosition()
    {
        // Arrange
        var position = PositionBase.Create(null, null);

        // Act
        var result = NwsEndpointPaths.PointPath(position, null);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void PointPath_ShouldReturnEmptyString_WhenEmptyStringPositionIsNullPosition()
    {
        // Arrange
        var position = PositionBase.Create(string.Empty, string.Empty);

        // Act
        var result = NwsEndpointPaths.PointPath(position, 12);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void PointPath_ShouldReturnEmptyString_WhenPositionIsNullAndLimitNull()
    {
        // Arrange
        var position = PositionBase.Create(null, null);

        // Act
        var result = NwsEndpointPaths.PointPath(position, null);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void PointPath_ShouldReturnEmptyString_WhenEmptyStringPositionIsNullAndLimitNull()
    {
        // Arrange
        var position = PositionBase.Create(string.Empty, string.Empty);

        // Act
        var result = NwsEndpointPaths.PointPath(position, 12);

        // Assert
        Assert.Equal(string.Empty, result);
    }
}
