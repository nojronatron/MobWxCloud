using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests.Library.MeasurementInheritors;

public class MinTemperatureLast24HoursTests
{
    [Fact]
    public void MinTemperatureLast24Hours_ToString_ValueIsNull_ReturnsEmptyString()
    {
        // Arrange
        var minTemp = new MinTemperatureLast24Hours { Value = null };

        // Act
        var result = minTemp.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void MinTemperatureLast24Hours_ToString_ValueIsNotNull_ReturnsFormattedString()
    {
        // Arrange
        var minTemp = new MinTemperatureLast24Hours { Value = 12.3456 };

        // Act
        var result = minTemp.ToString();

        // Assert
        Assert.Equal("12.35", result);
    }
}
