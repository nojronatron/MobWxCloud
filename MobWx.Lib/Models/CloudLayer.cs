namespace MobWx.Lib.Models;

using MobWx.Lib.Enums;
using System.Text.Json.Serialization;

public class CloudLayer
{
    [JsonPropertyName("base")]
    public QuantitativeValue? CloudBase { get; set; }

    [JsonPropertyName("amount")]
    public Amount? Amount { get; set; }

    public override string ToString()
    {
        string result = string.Empty;
        result += Amount is null
            ? "unk"
            : ((Amount).ToString());
        result += ", ";
        result += CloudBase is null
            ? "unk"
            : CloudBase.Value is null
                ? "unk"
                : CloudBase.Value.Value.ToString();
        return result;
    }
}
