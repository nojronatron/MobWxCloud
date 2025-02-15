using MobWx.Lib.Enums;
using MobWx.Lib.Models;
using MobWx.Lib.Models.Nws;
using System.Diagnostics;

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
            CloudBaseM = new QuantitativeValue { Value = 2740, UnitCode = "wmoUnit:m" }
        };

        // Act & Assert
        Assert.Null(nullCloudLayer.CloudBaseM);

        Assert.NotNull(noCloudLayer);
        Assert.Null(noCloudLayer.CloudBaseM);
        Assert.NotNull(noCloudLayer.Amount);
        Debug.WriteLine($"CloudLayerTest: No cloud layer amount: {noCloudLayer.Amount}");
        Assert.Equal(Amount.CLR, noCloudLayer.Amount);

        Assert.NotNull(ovcCloudLayer);
        Assert.NotNull(ovcCloudLayer.CloudBaseM);
        Assert.NotNull(ovcCloudLayer.Amount);
        Debug.WriteLine($"CloudLayerTest: Overcast layer amount: {ovcCloudLayer.Amount}");
        Assert.Equal(Amount.OVC, ovcCloudLayer.Amount);
    }

    [Theory]
    [InlineData(Amount.OVC, "OVC")]
    [InlineData(Amount.BKN, "BKN")]
    [InlineData(Amount.SCT, "SCT")]
    [InlineData(Amount.FEW, "FEW")]
    [InlineData(Amount.SKC, "SKC")]
    [InlineData(Amount.CLR, "CLR")]
    [InlineData(Amount.VV, "VV")]
    public void Amount_ToString_ShouldReturnExpectedString(Amount amount, string expected)
    {
        // Act
        var result = amount.ToString();

        // Assert
        Assert.Equal(expected, result);
    }
}
