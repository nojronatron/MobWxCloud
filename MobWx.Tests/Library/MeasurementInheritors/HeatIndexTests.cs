using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests.Library.MeasurementInheritors;

public class HeatIndexTests
{
    [Fact]
    public void HeatIndex_ToString_ValueIsNull_ReturnsEmptyString()
    {
        // Arrange
        var heatIndex = new HeatIndex { Value = null };

        // Act
        var result = heatIndex.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void HeatIndex_ToString_ValueIsNotNull_ReturnsFormattedString()
    {
        // Arrange
        var heatIndex = new HeatIndex { Value = 12.3456 };

        // Act
        var result = heatIndex.ToString();

        // Assert
        Assert.Equal("12.35", result);
    }
}
