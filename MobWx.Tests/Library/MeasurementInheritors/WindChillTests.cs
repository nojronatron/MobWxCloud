using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests.Library.MeasurementInheritors;

public class WindChillTests
{
    [Fact]
    public void WindChill_ToString_ValueIsNull_ReturnsEmptyString()
    {
        // Arrange
        var windChill = new WindChill { Value = null };

        // Act
        var result = windChill.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void WindChill_ToString_ValueIsNotNull_ReturnsFormattedString()
    {
        // Arrange
        var windChill = new WindChill { Value = 5.678 };

        // Act
        var result = windChill.ToString();

        // Assert
        Assert.Equal("5.7", result);
    }
}

