using System.Text;

namespace MobWx.Lib.Models.Nws;

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
        result.AppendLine($"Temperature: {TemperatureC}");
        result.AppendLine($"Dewpoint: {DewpointC}");
        result.AppendLine($"Wind Direction: {WindDirection}");
        result.AppendLine($"Wind Speed: {WindSpeedKph}");
        result.AppendLine($"Wind Gust: {WindGustKph}");
        result.AppendLine($"Barometric Pressure: {BarometricPressurePa}");
        result.AppendLine($"Visibility: {VisibilityM}");
        result.AppendLine($"Max Temp Last 24 Hours: {MaxTempCLast24Hours}");
        result.AppendLine($"Min Temp Last 24 Hours: {MinTempCLast24Hours}");
        result.AppendLine($"Precipitation Last Hour: {PrecipitationLastHourMm}");
        result.AppendLine($"Rh: {RhPercent}");
        result.AppendLine($"Wind Chill: {WindChillC}");
        result.AppendLine($"Heat Index: {HeatIndexC}");

        if (CloudLayers is not null && CloudLayers.Count > 0)
        {
            result.AppendLine("Cloud Layers:");
            foreach (var cloudLayer in CloudLayers)
            {
                result.AppendLine($"  {cloudLayer}");
            }
        }

        result.AppendLine("***** End Observation *****");
        return result.ToString();
    }
}
