using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests
{
    public class PositionTests
    {
        [Fact]
        public void IsEmpty_ShouldReturnTrue_WhenLatitudeAndLongitudeAreEmpty()
        {
            // Arrange
            // Act
            var position = Position.CreatePosition(string.Empty, string.Empty);

            // Assert
            Assert.True(position is NullPosition);
        }

        [Fact]
        public void IsEmpty_ShouldReturnFalse_WhenLatitudeAndLongitudeAreNotEmpty()
        {
            // Arrange
            // Act
            var position = Position.CreatePosition("45.0", "90.0");

            // Assert
            Assert.True(position is not NullPosition);
        }

        [Theory]
        [InlineData("45.0", true)]
        [InlineData("-90.0", true)]
        [InlineData("90.0", true)]
        [InlineData("91.0", false)]
        [InlineData("-91.0", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValidLatitude_ShouldReturnExpectedResult(string latitude, bool expected)
        {
            // Act
            var result = Position.IsValidLatitude(latitude);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("90.0", true)]
        [InlineData("-180.0", true)]
        [InlineData("180.0", true)]
        [InlineData("181.0", false)]
        [InlineData("-181.0", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void IsValidLongitude_ShouldReturnExpectedResult(string longitude, bool expected)
        {
            // Act
            var result = Position.IsValidLongitude(longitude);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("45.12345", "45.1234")]
        [InlineData("45.1", "45.1")]
        [InlineData("45.123", "45.123")]
        [InlineData("45.1234", "45.1234")]
        public void LimitToFourDecimalPlaces_ShouldReturnExpectedResult(string coordinate, string expected)
        {
            // Act
            var result = Position.LimitToFourDecimalPlaces(coordinate);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void CreatePosition_ShouldReturnNullPosition_WhenLatitudeOrLongitudeIsEmpty()
        {
            // Act
            var result = Position.CreatePosition(string.Empty, "90.0");

            // Assert
            Assert.IsType<NullPosition>(result);
        }

        [Fact]
        public void CreatePosition_ShouldReturnPosition_WhenLatitudeAndLongitudeAreValid()
        {
            // Act
            var result = Position.CreatePosition("45.0", "90.0");

            // Assert
            Assert.IsType<Position>(result);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var position = Position.CreatePosition("45.0", "90.0");

            // Act
            var result = position.ToString();

            // Assert
            Assert.Equal("45.0,90.0", result);
        }
    }
}
