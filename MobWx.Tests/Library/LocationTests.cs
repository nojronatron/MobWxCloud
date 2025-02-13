using MobWx.Lib.Models.Geocoding;
using Xunit;

namespace MobWx.Tests.Library;

public class LocationTests
{
    [Fact]
    public void Location_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        double lat = 40.7128;
        double lon = -74.0060;

        // Act
        var location = new Location(lat, lon);

        // Assert
        Assert.NotNull(location.Lat);
        Assert.NotNull(location.Lon);
        Assert.Equal(lat, location.Lat.Value);
        Assert.Equal(lon, location.Lon.Value);
        Assert.Equal("United States", location.Country);
    }

    [Fact]
    public void Location_HasNulls_ShouldReturnTrue_WhenLatOrLonIsNull()
    {
        // Arrange
        var location = new Location(0, 0) { Lat = null, Lon = null };

        // Act
        var result = location.HasNulls();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Location_HasNulls_ShouldReturnFalse_WhenLatAndLonAreNotNull()
    {
        // Arrange
        var location = new Location(40.7128, -74.0060);

        // Act
        var result = location.HasNulls();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Location_Equals_ShouldReturnTrue_WhenLocationsAreEqual()
    {
        // Arrange
        var location1 = new Location(40.7128, -74.0060)
        {
            CityName = "New York",
            StateAbbreviation = "NY"
        };
        var location2 = new Location(40.7128, -74.0060)
        {
            CityName = "New York",
            StateAbbreviation = "NY"
        };

        // Act
        var result = location1.Equals(location2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Location_Equals_ShouldReturnFalse_WhenLocationsAreNotEqual()
    {
        // Arrange
        var location1 = new Location(40.7128, -74.0060)
        {
            CityName = "New York",
            StateAbbreviation = "NY"
        };
        var location2 = new Location(34.0522, -118.2437)
        {
            CityName = "Los Angeles",
            StateAbbreviation = "CA"
        };

        // Act
        var result = location1.Equals(location2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Location_Equals_ShouldReturnFalse_WhenOtherLocationIsNull()
    {
        // Arrange
        var location = new Location(40.7128, -74.0060)
        {
            CityName = "New York",
            StateAbbreviation = "NY"
        };

        // Act
        var result = location.Equals(null);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Location_GetHashCode_ShouldReturnExpectedHashCode()
    {
        // Arrange
        var location = new Location(40.7128, -74.0060)
        {
            CityName = "New York",
            StateAbbreviation = "NY"
        };

        // Act
        var hashCode = location.GetHashCode();

        // Assert
        Assert.Equal(HashCode.Combine(location.Lat, location.Lon, location.CityName, location.StateAbbreviation, location.Country), hashCode);
    }
}
