using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests.Library.MeasurementInheritors;

public class WindGustTests
{
    [Fact]
    public void WindGust_ToString_ValueIsNull_ReturnsEmptyString()
    {
        // Arrange
        var windGust = new WindGust { Value = null };

        // Act
        var result = windGust.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void WindGust_ToString_ValueIsNotNull_ReturnsFormattedString()
    {
        // Arrange
        var windGust = new WindGust { Value = 15.678 };

        // Act
        var result = windGust.ToString();

        // Assert
        Assert.Equal("15.7", result);
    }
}
