using MobWx.Lib.Models.Geocoding;
using Xunit;

namespace MobWx.Tests.Library
{
    public class LocationTests
    {
        [Fact]
        public void GetStateName_ReturnsCorrectStateName()
        {
            // Arrange
            var location = new Location
            {
                License = "Some License",
                DisplayName = "City, State, Country"
            };

            // Act
            var stateName = location.GetStateName();

            // Assert
            Assert.Equal("State", stateName);
        }

        [Fact]
        public void GetStateName_ReturnsEmptyString_WhenLicenseIsEmpty()
        {
            // Arrange
            var location = new Location
            {
                License = string.Empty,
                DisplayName = "City, State, Country"
            };

            // Act
            var stateName = location.GetStateName();

            // Assert
            Assert.Equal(string.Empty, stateName);
        }

        [Fact]
        public void Create_ReturnsNewLocationInstance()
        {
            // Arrange
            var city = "City";
            var state = "ST";
            var license = "Some License";
            var displayName = "City, State, Country";

            // Act
            var location = Location.Create(city, state, license, displayName);

            // Assert
            Assert.Equal(city, location.CityName);
            Assert.Equal(state, location.StateAbbreviation);
            Assert.Equal(license, location.License);
            Assert.Equal(displayName, location.DisplayName);
        }

        [Fact]
        public void Equals_ReturnsTrue_WhenLocationsAreEqual()
        {
            // Arrange
            var location1 = new Location
            {
                CityName = "City",
                StateAbbreviation = "ST"
            };
            var location2 = new Location
            {
                CityName = "City",
                StateAbbreviation = "ST"
            };

            // Act
            var result = location1.Equals(location2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenLocationsAreNotEqual()
        {
            // Arrange
            var location1 = new Location
            {
                CityName = "City1",
                StateAbbreviation = "ST1"
            };
            var location2 = new Location
            {
                CityName = "City2",
                StateAbbreviation = "ST2"
            };

            // Act
            var result = location1.Equals(location2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenOtherIsNull()
        {
            // Arrange
            var location = new Location
            {
                CityName = "City",
                StateAbbreviation = "ST"
            };

            // Act
            var result = location.Equals((Location)null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EqualsObject_ReturnsTrue_WhenObjectsAreEqual()
        {
            // Arrange
            var location1 = new Location
            {
                CityName = "City",
                StateAbbreviation = "ST"
            };
            var location2 = new Location
            {
                CityName = "City",
                StateAbbreviation = "ST"
            };

            // Act
            var result = location1.Equals((object)location2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EqualsObject_ReturnsFalse_WhenObjectsAreNotEqual()
        {
            // Arrange
            var location1 = new Location
            {
                CityName = "City1",
                StateAbbreviation = "ST1"
            };
            var location2 = new Location
            {
                CityName = "City2",
                StateAbbreviation = "ST2"
            };

            // Act
            var result = location1.Equals((object)location2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_ReturnsSameHashCode_ForEqualObjects()
        {
            // Arrange
            var location1 = new Location
            {
                CityName = "City",
                StateAbbreviation = "ST"
            };
            var location2 = new Location
            {
                CityName = "City",
                StateAbbreviation = "ST"
            };

            // Act
            var hashCode1 = location1.GetHashCode();
            var hashCode2 = location2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_ReturnsDifferentHashCode_ForDifferentObjects()
        {
            // Arrange
            var location1 = new Location
            {
                CityName = "City1",
                StateAbbreviation = "ST1"
            };
            var location2 = new Location
            {
                CityName = "City2",
                StateAbbreviation = "ST2"
            };

            // Act
            var hashCode1 = location1.GetHashCode();
            var hashCode2 = location2.GetHashCode();

            // Assert
            Assert.NotEqual(hashCode1, hashCode2);
        }
    }
}
