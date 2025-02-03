using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests.Library.MeasurementInheritors;

public class PrecipitationLastHourTests
{
    [Fact]
    public void PrecipitationLastHour_ToString_ValueIsNull_ReturnsEmptyString()
    {
        // Arrange
        var precipitation = new PrecipitationLastHour { Value = null };

        // Act
        var result = precipitation.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void PrecipitationLastHour_ToString_ValueIsNotNull_ReturnsStringValue()
    {
        // Arrange
        var precipitation = new PrecipitationLastHour { Value = 10 };

        // Act
        var result = precipitation.ToString();

        // Assert
        Assert.Equal("10", result);
    }
}
