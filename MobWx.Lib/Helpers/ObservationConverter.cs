using MobWx.Lib.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MobWx.Lib.Helpers;

public class ObservationConverter : JsonConverter<Observation>
{
    /// <summary>
    /// Read the JSON and convert it to an Observation object using property specific JSON Serialization.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override Observation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var observation = new Observation();
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            JsonElement root = doc.RootElement;
            if (root.TryGetProperty("id", out JsonElement idElement))
            {
                observation.Id = idElement.GetString() ?? string.Empty;
            }
            if (root.TryGetProperty("station", out JsonElement stationElement))
            {
                observation.Station = stationElement.GetString() ?? string.Empty;
            }
            if (root.TryGetProperty("timestamp", out JsonElement timestampElement))
            {
                observation.Timestamp = timestampElement.GetDateTime();
            }
            if (root.TryGetProperty("rawMessage", out JsonElement rawMessageElement))
            {
                observation.RawMessage = rawMessageElement.GetString() ?? string.Empty;
            }
            if (root.TryGetProperty("textDescription", out JsonElement textDescriptionElement))
            {
                observation.TextDescription = textDescriptionElement.GetString() ?? string.Empty;
            }
            if (root.TryGetProperty("icon", out JsonElement iconElement))
            {
                observation.Icon = iconElement.GetString() ?? string.Empty;
            }
            // Add other properties as needed
        }
        return observation;
    }

    public override void Write(Utf8JsonWriter writer, Observation value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
