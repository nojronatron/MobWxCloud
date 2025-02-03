using MobWx.Lib.Models;

namespace MobWx.Tests.Library.MeasurementInheritors;

public class DewpointTests
{
    [Fact]
    public void Dewpoint_ToString_ValueIsNull_ReturnsEmptyString()
    {
        // Arrange
        var dewpoint = new Dewpoint { Value = null };

        // Act
        var result = dewpoint.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Dewpoint_ToString_ValueIsNotNull_ReturnsFormattedString()
    {
        // Arrange
        var dewpoint = new Dewpoint { Value = 12.345 };

        // Act
        var result = dewpoint.ToString();

        // Assert
        Assert.Equal("12.3", result);
    }
}
