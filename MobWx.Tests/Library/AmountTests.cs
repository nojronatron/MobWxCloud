using MobWx.Lib.Models;
using Xunit;

namespace MobWx.Tests.Library;

public class AmountTests
{
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
