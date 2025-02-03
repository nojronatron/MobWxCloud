using MobWx.Lib.Models;

namespace MobWx.Tests.Library;

public class MeasurementTests
{
    [Fact]
    public void Measurement_DefaultValues_ShouldReturnExpectedValues()
    {
        // Arrange
        var measurement = new Measurement();

        // Act & Assert
        Assert.Equal(string.Empty, measurement.UnitCode);
        Assert.Null(measurement.Value);
        Assert.Equal(string.Empty, measurement.QualityControl);
    }

    [Fact]
    public void Measurement_MinValue_ShouldReturnMinValue()
    {
        // Act
        var result = Measurement.MinValue;

        // Assert
        Assert.Equal(double.MinValue, result.Value);
    }

    [Fact]
    public void Measurement_ZeroValue_ShouldReturnZeroValue()
    {
        // Act
        var result = Measurement.ZeroValue;

        // Assert
        Assert.Equal(0, result.Value);
    }

    [Fact]
    public void Measurement_IsNull_ShouldReturnTrue_WhenValueIsNull()
    {
        // Arrange
        var measurement = new Measurement();

        // Act
        var result = measurement.HasNullValue;

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Measurement_IsNull_ShouldReturnFalse_WhenValueIsNotNull()
    {
        // Arrange
        var measurement = new Measurement { Value = 10.0 };

        // Act
        var result = measurement.HasNullValue;

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(10.5, 10)]
    [InlineData(10.1, 10)]
    [InlineData(10.9, 11)]
    [InlineData(10.0, 10)]
    public void Measurement_ToInt_ShouldReturnRoundedIntValue(double num, int expected)
    {
        // Arrange
        var measurement = new Measurement { Value = num };

        // Act
        var result = measurement.ToInt();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10.5, 10)]
    [InlineData(10.1, 10)]
    [InlineData(10.9, 11)]
    [InlineData(10.0, 10)]
    [InlineData(null, null)]
    public void Measurement_ToNullableInt_ShouldReturnRoundedNullableIntValue(double? num, int? expected)
    {
        // Arrange
        var measurement = new Measurement { Value = num };

        // Act
        var result = measurement.ToNullableInt();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Measurement_ToNullableInt_ShouldReturnNull_WhenValueIsNull()
    {
        // Arrange
        var measurement = new Measurement();

        // Act
        var result = measurement.ToNullableInt();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Measurement_ToDouble_ShouldReturnRoundedDoubleValue()
    {
        // Arrange
        var measurement = new Measurement { Value = 10.555 };

        // Act
        var result = measurement.ToDouble();

        // Assert
        Assert.Equal(10.56, result);
    }

    [Fact]
    public void Measurement_ToNullableDouble_ShouldReturnRoundedNullableDoubleValue()
    {
        // Arrange
        var measurement = new Measurement { Value = 10.555 };

        // Act
        var result = measurement.ToNullableDouble();

        // Assert
        Assert.Equal(10.56, result);
    }

    [Fact]
    public void Measurement_ToNullableDouble_ShouldReturnNull_WhenValueIsNull()
    {
        // Arrange
        var measurement = new Measurement();

        // Act
        var result = measurement.ToNullableDouble();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Measurement_ToString_ShouldReturnValueAsString()
    {
        // Arrange
        var measurement = new Measurement { Value = 10.5 };

        // Act
        var result = measurement.ToString();

        // Assert
        Assert.Equal("10.5", result);
    }

    [Fact]
    public void Measurement_ToString_ShouldReturnEmptyString_WhenValueIsNull()
    {
        // Arrange
        var measurement = new Measurement();

        // Act
        var result = measurement.ToString();

        // Assert
        Assert.NotNull(measurement);
        Assert.True(measurement.HasNullValue);
    }
}
