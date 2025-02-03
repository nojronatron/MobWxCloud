using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests.Library.MeasurementInheritors;

public class WindSpeedTests
{
    [Fact]
    public void WindSpeed_ToString_ValueIsNull_ReturnsEmptyString()
    {
        // Arrange
        var windSpeed = new WindSpeed { Value = null };

        // Act
        var result = windSpeed.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void WindSpeed_ToString_ValueIsNotNull_ReturnsFormattedString()
    {
        // Arrange
        var windSpeed = new WindSpeed { Value = 12.3456 };

        // Act
        var result = windSpeed.ToString();

        // Assert
        Assert.Equal("12.35", result);
    }
}
