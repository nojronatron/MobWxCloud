using MobWx.API.Common;
using MobWx.Lib.Models;

namespace MobWx.Tests.Library;

public class NwsEndpointPathsTests
{
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
    [InlineData("40.7128", "-74.0060", "/points/40.7128,-74.0060")]
    [InlineData("34.0522", "-118.2437", "/points/34.0522,-118.2437")]
    public void PointPath_ShouldReturnExpectedPath(string latitude, string longitude, string expected)
    {
        // Arrange
        var position = Position.Create(latitude, longitude);

        // Act
        var result = NwsEndpointPaths.PointPath(position);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void PointPath_ShouldReturnEmptyString_WhenPositionIsNullPosition()
    {
        // Arrange
        var position = Position.Create(null, null);

        // Act
        var result = NwsEndpointPaths.PointPath(position);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void PointPath_ShouldReturnEmptyString_WhenEmptyStringPositionIsNullPosition()
    {
        // Arrange
        var position = Position.Create(string.Empty, string.Empty);

        // Act
        var result = NwsEndpointPaths.PointPath(position);

        // Assert
        Assert.Equal(string.Empty, result);
    }
}
