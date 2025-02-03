using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests.Library.MeasurementInheritors;

public class MaxTemperatureLast24HoursTests
{
    [Fact]
    public void MaxTemperatureLast24Hours_ToString_ValueIsNull_ReturnsEmptyString()
    {
        // Arrange
        var maxTemp = new MaxTemperatureLast24Hours { Value = null };

        // Act
        var result = maxTemp.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void MaxTemperatureLast24Hours_ToString_ValueIsNotNull_ReturnsFormattedString()
    {
        // Arrange
        var maxTemp = new MaxTemperatureLast24Hours { Value = 25.6789 };

        // Act
        var result = maxTemp.ToString();

        // Assert
        Assert.Equal("25.68", result);
    }

    [Fact]
    public void MaxTemperatureLast24Hours_ToString_UnitCodeIsNullOrWhitespace_SetsUnitCodeToNull()
    {
        // Arrange
        var maxTemp = new MaxTemperatureLast24Hours { Value = 25.6789, UnitCode = null };

        // Act
        var result = maxTemp.ToString();

        // Assert
        Assert.Equal("25.68", result);
        Assert.Equal(":null", maxTemp.UnitCode);
    }
}
