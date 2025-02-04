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
        result.AppendLine($"Temperature: {Temperature}");
        result.AppendLine($"Dewpoint: {Dewpoint}");
        result.AppendLine($"Wind Direction: {WindDirection}");
        result.AppendLine($"Wind Speed: {WindSpeed}");
        result.AppendLine($"Wind Gust: {WindGust}");
        result.AppendLine($"Barometric Pressure: {BarometricPressureHpa}");
        result.AppendLine($"Visibility: {Visibility}");
        result.AppendLine($"Max Temp Last 24 Hours: {MaxTemperatureLast24Hours}");
        result.AppendLine($"Min Temp Last 24 Hours: {MinTemperatureLast24Hours}");
        result.AppendLine($"Precipitation Last Hour: {PrecipitationLastHour}");
        result.AppendLine($"Wind Chill: {WindChill}");
        result.AppendLine($"Heat Index: {HeatIndex}");
        
        if (CloudLayers is not null && CloudLayers.Count > 0){
        }

        result.AppendLine("***** End Observation *****");
        return result.ToString();
    }
}
