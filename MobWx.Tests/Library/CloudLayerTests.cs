using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests.Library;

public class CloudLayerTests
{
    [Fact]
    public void CloudLayer_DefaultValues_ShouldReturnExpectedValues()
    {
        // Arrange
        var cloudLayer = new CloudLayer();

        // Act & Assert
        Assert.Equal(Measurement.ZeroValue, cloudLayer.Base);
        Assert.Equal(Amount.OVC, cloudLayer.Amount); // Default enum value is the first one
    }

    [Fact]
    public void CloudLayer_SetValues_ShouldReturnExpectedValues()
    {
        // Arrange
        var measurement = new Measurement { Value = 1000, UnitCode = "wmoUnit:m" };
        var cloudLayer = new CloudLayer
        {
            Base = measurement,
            Amount = Amount.SCT
        };

        // Act & Assert
        Assert.Equal(measurement, cloudLayer.Base);
        Assert.Equal(Amount.SCT, cloudLayer.Amount);
    }
}
