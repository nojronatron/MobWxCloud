using MobWx.Lib.Enumerations;
using MobWx.Lib.Models;

namespace MobWx.Tests.Library;

public class CloudLayerTests
{
    [Fact]
    public void CloudLayer_DefaultValues_ShouldReturnExpectedValues()
    {
        // Arrange
        var nullCloudLayer = new CloudLayer();

        var noCloudLayer = new CloudLayer()
        {
            Amount = Amount.CLR
        };

        var ovcCloudLayer = new CloudLayer()
        {
            Amount = Amount.OVC,
            CloudBase = new MeasurementInt { Value = 2740, UnitCode = "wmoUnit:m" }
        };

        // Act & Assert
        Assert.Null(nullCloudLayer.CloudBase);

        Assert.NotNull(noCloudLayer);
        Assert.Null(noCloudLayer.CloudBase);
        Assert.Equal(Amount.CLR, noCloudLayer.Amount);

        Assert.NotNull(ovcCloudLayer);
        Assert.NotNull(ovcCloudLayer.CloudBase);
        Assert.Equal(Amount.OVC, ovcCloudLayer.Amount);
    }

}
