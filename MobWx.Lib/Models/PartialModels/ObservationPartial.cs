using System.Text;

namespace MobWx.Lib.Models;

public partial class Observation
{
    public override string ToString()
    {
        StringBuilder result = new();
        result.AppendLine("***** Observation *****");
        result.AppendLine($"Id: {Id}");
        result.AppendLine($"Station: {Station}");
        DateTime timestampVal = Timestamp ?? DateTime.MinValue;
        result.AppendLine($"Timestamp: {timestampVal}");
        result.AppendLine($"Raw Message: {RawMessage}");
        result.AppendLine($"Text Description: {TextDescription}");
        result.AppendLine($"Icon: {Icon}");
        Measurement temperatureC = Temperature ?? Measurement.MinValue;
        result.AppendLine($"Temperature: {temperatureC}");
        Measurement dewpointC = Dewpoint ?? Measurement.MinValue;
        result.AppendLine($"Dewpoint: {dewpointC}");
        result.AppendLine($"Wind Direction: {WindDirection}");
        result.AppendLine($"Wind Speed: {WindSpeed}");
        result.AppendLine($"Wind Gust: {WindGust}");
        Measurement barometricPressureVal = BarometricPressureMb ?? Measurement.MinValue;
        result.AppendLine($"Barometric Pressure: {barometricPressureVal}");
        Measurement visibilityVal = Visibility ?? Measurement.MinValue;
        result.AppendLine($"Visibility: {visibilityVal}");
        Measurement maxTempLast24HrsVal = MaxTemperatureLast24Hours ?? Measurement.MinValue;
        result.AppendLine($"Max Temp Last 24 Hours: {maxTempLast24HrsVal}");
        Measurement minTempLast24HrsVal = MinTemperatureLast24Hours ?? Measurement.MinValue;
        result.AppendLine($"Min Temp Last 24 Hours: {minTempLast24HrsVal}");
        Measurement precipLastHourVal = PrecipitationLastHour ?? Measurement.MinValue;
        result.AppendLine($"Precipitation Last Hour: {precipLastHourVal}");
        Measurement windChillC = WindChill ?? Measurement.MinValue;
        result.AppendLine($"Wind Chill: {windChillC}");
        Measurement heatIndexC = HeatIndex ?? Measurement.MinValue;
        result.AppendLine($"Heat Index: {heatIndexC}");
        result.AppendLine("***** End Observation *****");
        return result.ToString();
    }
}
