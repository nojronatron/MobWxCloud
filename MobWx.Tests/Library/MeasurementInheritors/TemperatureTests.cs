using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests.Library.MeasurementInheritors;

public class TemperatureTests
{
    [Fact]
    public void Temperature_ToString_ValueIsNull_ReturnsEmptyString()
    {
        // Arrange
        var temperature = new Temperature { Value = null };

        // Act
        var result = temperature.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Temperature_ToString_ValueIsNotNull_ReturnsFormattedString()
    {
        // Arrange
        var temperature = new Temperature { Value = 23.456 };

        // Act
        var result = temperature.ToString();

        // Assert
        Assert.Equal("23.5", result);
    }
}

