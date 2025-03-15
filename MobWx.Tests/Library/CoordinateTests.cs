using MobWx.Lib.Models;

namespace MobWx.Tests.Library;

public class CoordinateTests
{
    [Fact]
    public void Coordinate_Create_ShouldInitializeProperties()
    {
        // Arrange
        decimal lat = 40.7128m;
        decimal lon = -74.0060m;

        // Act
        var coordinate = Coordinate.Create(lat, lon);

        // Assert
        Assert.NotNull(coordinate.Lat);
        Assert.NotNull(coordinate.Lon);
        Assert.Equal(lat, coordinate.Lat.Value);
        Assert.Equal(lon, coordinate.Lon.Value);
    }

    [Fact]
    public void Coordinate_HasNulls_ShouldReturnFalse_WhenLatAndLonAreNotNull()
    {
        // Arrange
        var coordinate = Coordinate.Create(40.7128m, -74.0060m);

        // Act
        var result = coordinate.HasNulls();

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(40.7128, 2, 40.71)]
    [InlineData(-74.0060, 2, -74.01)]
    [InlineData(123.456789, 4, 123.4568)]
    public void Coordinate_LimitDecimalPlaces_ShouldReturnExpectedValue(decimal num, int places, decimal expected)
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
        decimal lat = 40.7128m;
        decimal lon = -74.0060m;
        var coordinate1 = Coordinate.Create(lat, lon);
        var coordinate2 = Coordinate.Create(lat, lon);

        // Act
        var result = coordinate1.Equals(coordinate2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Coordinate_Equals_ShouldReturnFalse_WhenCoordinatesAreNotEqual()
    {
        // Arrange
        decimal firstLat = 40.7128m;
        decimal firstLon = -74.0060m;
        decimal secondLat = 34.0622m;
        decimal secondLon = -118.2437m;
        var coordinate1 = Coordinate.Create(firstLat, firstLon);
        var coordinate2 = Coordinate.Create(secondLat, secondLon);

        // Act
        var result = coordinate1.Equals(coordinate2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Coordinate_Equals_ShouldReturnFalse_WhenOtherCoordinateIsNull()
    {
        // Arrange
        var coordinate = Coordinate.Create(40.7128m, -74.0060m);

        // Act
        var result = coordinate.Equals(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Coordinate_GetHashCode_ShouldReturnExpectedHashCode()
    {
        // Arrange
        var coordinate = Coordinate.Create(40.7128m, -74.0060m);

        // Act
        var hashCode = coordinate.GetHashCode();

        // Assert
        Assert.Equal(HashCode.Combine(coordinate.Lat, coordinate.Lon), hashCode);
    }

    [Theory]
    [InlineData(45.0, true)]
    [InlineData(-90.0, true)]
    [InlineData(90.0, true)]
    [InlineData(91.0, false)]
    [InlineData(-91.0, false)]
    [InlineData((double)default, true)]
    public void IsValidLatitude_ShouldReturnExpectedResult(decimal latitude, bool expected)
    {
        // Arrange
        Coordinate coordinate = Coordinate.Create(latitude, 0);

        // Act
        var result = coordinate.HasValidLatitude();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(90.0, true)]
    [InlineData(-180.0, true)]
    [InlineData(180.0, true)]
    [InlineData(181.0, false)]
    [InlineData(-181.0, false)]
    [InlineData((double)default, true)]
    public void IsValidLongitude_ShouldReturnExpectedResult(decimal longitude, bool expected)
    {
        // Arrange
        Coordinate coordinate = Coordinate.Create(0, longitude);

        // Act
        var result = coordinate.HasValidLongitude();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData((double)45, 0, 45)]
    [InlineData(45d, 0, 45d)]
    [InlineData(45.123456789, 1, 45.1)]
    [InlineData(45.123456789, 2, 45.12)]
    [InlineData(45.123456789, 3, 45.123)]
    [InlineData(45.123432123, 4, 45.1234)]
    [InlineData(45.123456789, 4, 45.1235)]
    [InlineData(45.123456789, 5, 45.12346)]
    [InlineData(45.123456789, 6, 45.123457)]
    [InlineData(45.123456789, 7, 45.1234568)]
    public void LimitToFourDecimalPlaces_ShouldReturnExpectedResult(decimal number, int limit, decimal expected)
    {
        // Act
        var result = Coordinate.LimitDecimalPlaces(number, limit);

        // Assert
        Assert.Equal(expected, result, precision: limit);
    }

    [Theory]
    [InlineData(45.12345, -1)]
    [InlineData(45.12345, -2)]
    [InlineData(45.12345, 8)]
    [InlineData(45.12345, 9)]
    public void LimitDecimalPlaces_ShouldThrowIfNotInRange0To7(decimal number, int limit)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Coordinate.LimitDecimalPlaces(number, limit));
    }
}
