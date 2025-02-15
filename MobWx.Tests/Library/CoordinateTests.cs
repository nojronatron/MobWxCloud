using MobWx.Lib.Models;

namespace MobWx.Tests.Library;

public class CoordinateTests
{
    [Fact]
    public void Coordinate_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        double lat = 40.7128;
        double lon = -74.0060;

        // Act
        var coordinate = new Coordinate(lat, lon);

        // Assert
        Assert.NotNull(coordinate.Lat);
        Assert.NotNull(coordinate.Lon);
        Assert.Equal(lat, coordinate.Lat.Value);
        Assert.Equal(lon, coordinate.Lon.Value);
    }

    [Fact]
    public void Coordinate_ToPosition_ShouldReturnPositionWithoutTrailingZeroes()
    {
        // Arrange
        var coordinate = new Coordinate(40.7128, -74.0060);

        // Act
        var result = coordinate.ToPosition();

        // Assert
        Assert.IsType<Position>(result);
        Assert.Equal("40.7128", result.Latitude);
        Assert.NotEqual("-74.0060", result.Longitude);
        Assert.Equal("-74.006", result.Longitude);
    }

    [Fact]
    public void Coordinate_ToPosition_NullLatLon_ShouldReturnNullPosition()
    {
        // Arrange
        var coordinate = new Coordinate(0, 0) { Lat = null, Lon = null };

        // Act
        var result = coordinate.ToPosition();

        // Assert
        Assert.IsType<NullPosition>(result);
    }

    [Fact]
    public void Coordinate_HasNulls_ShouldReturnTrue_WhenLatOrLonIsNull()
    {
        // Arrange
        var coordinate = new Coordinate(0, 0) { Lat = null, Lon = null };

        // Act
        var result = coordinate.HasNulls();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Coordinate_HasNulls_ShouldReturnFalse_WhenLatAndLonAreNotNull()
    {
        // Arrange
        var coordinate = new Coordinate(40.7128, -74.0060);

        // Act
        var result = coordinate.HasNulls();

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(40.7128, 2, 40.71)]
    [InlineData(-74.0060, 2, -74.01)]
    [InlineData(123.456789, 4, 123.4568)]
    public void Coordinate_LimitDecimalPlaces_ShouldReturnExpectedValue(double num, int places, double expected)
    {
        // Act
        var result = Coordinate.LimitDecimalPlaces(num, places);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Coordinate_Equals_ShouldReturnTrue_WhenCoordinatesAreEqual()
    {
        // Arrange
        var coordinate1 = new Coordinate(40.7128, -74.0060);
        var coordinate2 = new Coordinate(40.7128, -74.0060);

        // Act
        var result = coordinate1.Equals(coordinate2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Coordinate_Equals_ShouldReturnFalse_WhenCoordinatesAreNotEqual()
    {
        // Arrange
        var coordinate1 = new Coordinate(40.7128, -74.0060);
        var coordinate2 = new Coordinate(34.0522, -118.2437);

        // Act
        var result = coordinate1.Equals(coordinate2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Coordinate_Equals_ShouldReturnFalse_WhenOtherCoordinateIsNull()
    {
        // Arrange
        var coordinate = new Coordinate(40.7128, -74.0060);

        // Act
        var result = coordinate.Equals(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Coordinate_GetHashCode_ShouldReturnExpectedHashCode()
    {
        // Arrange
        var coordinate = new Coordinate(40.7128, -74.0060);

        // Act
        var hashCode = coordinate.GetHashCode();

        // Assert
        Assert.Equal(HashCode.Combine(coordinate.Lat?.Value, coordinate.Lon?.Value), hashCode);
    }

    [Theory]
    [InlineData(90.0, 90.0)]
    [InlineData(-90.0, -90.0)]
    [InlineData(180.0, 90.0)]
    [InlineData(-180.0, -90.0)]
    public void Latitude_GetValue_ShouldReturnExpectedValue(double input, double expected)
    {
        // Arrange
        var latitude = new Latitude { Value = input };

        // Act
        var result = latitude.GetValue();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(180.0, 180.0)]
    [InlineData(-180.0, -180.0)]
    [InlineData(360.0, 180.0)]
    [InlineData(-360.0, -180.0)]
    public void Longitude_GetValue_ShouldReturnExpectedValue(double input, double expected)
    {
        // Arrange
        var longitude = new Longitude { Value = input };

        // Act
        var result = longitude.GetValue();

        // Assert
        Assert.Equal(expected, result);
    }
}
