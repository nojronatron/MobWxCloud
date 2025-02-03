using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests.Library.MeasurementInheritors;

public class WindDirectionTests
{
    [Fact]
    public void WindDirection_ToString_ValueIsNull_ReturnsEmptyString()
    {
        // Arrange
        var windDirection = new WindDirection { Value = null };

        // Act
        var result = windDirection.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void WindDirection_ToString_ValueIsNotNull_ReturnsStringValue()
    {
        // Arrange
        var windDirection = new WindDirection { Value = 180 };

        // Act
        var result = windDirection.ToString();

        // Assert
        Assert.Equal("180", result);
    }
}

