using MobWx.Lib.Models;
using MobWx.Lib.Models.Geocoding;

namespace MobWx.Tests.Library;

public class PositionTests
{
    [Fact]
    public void HasCoordinates_ReturnsTrue_WhenCoordinateIsValid()
    {
        // Arrange
        var coordinate = Coordinate.Create(40.7128m, -74.0060m);
        var position = Position.Create(coordinate);

        // Act
        var result = position.HasCoordinates;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasCoordinates_ReturnsFalse_WhenCoordinateIsNull()
    {
        // Arrange
        // Note: This is an anti-pattern at this point
        var coordinate = new Coordinate { Lat = null, Lon = null };
        var position = Position.Create(coordinate); 

        // Act
        var result = position.HasCoordinates;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasLocation_ReturnsTrue_WhenLocationIsValid()
    {
        // Arrange
        var location = Location.Create("New York", "NY", "test-license", "test-display-name");
        var position = Position.Create(location);

        // Act
        var result = position.HasLocation;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasLocation_ReturnsFalse_WhenLocationIsNull()
    {
        // Arrange
        var position = new Position();

        // Act
        var result = position.HasLocation;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasLocation_ReturnsFalse_WhenLocationHasEmptyStringValues()
    {
        // Arrange
        var location = Location.Create(string.Empty, string.Empty, string.Empty, string.Empty);
        var position = Position.Create(location);

        // Act
        var result = position.HasLocation;

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Create_WithCoordinateAndLocation_ReturnsNewPositionInstance()
    {
        // Arrange
        var coordinate = Coordinate.Create(40.7128m, -74.0060m);
        var location = Location.Create("New York", "NY", "test-license", "test-display-name");

        // Act
        var position = Position.Create(coordinate, location);

        // Assert
        Assert.Equal(coordinate, position.Coordinate);
        Assert.Equal(location, position.Location);
    }

    [Fact]
    public void Create_WithCoordinateOnly_ReturnsNewPositionInstance()
    {
        // Arrange
        var coordinate = Coordinate.Create(40.7128m, -74.0060m);

        // Act
        var position = Position.Create(coordinate);

        // Assert
        Assert.Equal(coordinate, position.Coordinate);
        Assert.Null(position.Location);
    }

    [Fact]
    public void Create_WithLocationOnly_ReturnsNewPositionInstance()
    {
        // Arrange
        var location = Location.Create("City of New York", "New York", "license text", "City of New York, New York (Manhattan), New York, United States");

        // Act
        var position = Position.Create(location);

        // Assert
        Assert.Equal(location, position.Location);
        Assert.Null(position.Coordinate);
    }
}
