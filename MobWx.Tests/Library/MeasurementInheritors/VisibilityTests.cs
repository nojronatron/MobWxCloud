using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests.Library.MeasurementInheritors;

public class VisibilityTests
{
    [Fact]
    public void Visibility_ToString_ValueIsNull_ReturnsEmptyString()
    {
        // Arrange
        var visibility = new Visibility { Value = null };

        // Act
        var result = visibility.ToString();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Visibility_ToString_ValueIsNotNull_ReturnsStringValue()
    {
        // Arrange
        var visibility = new Visibility { Value = 1000 };

        // Act
        var result = visibility.ToString();

        // Assert
        Assert.Equal("1000", result);
    }
}
