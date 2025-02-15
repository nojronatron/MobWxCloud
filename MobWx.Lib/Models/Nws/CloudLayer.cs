using MobWx.Lib.Enums;
using System.Text.Json.Serialization;

namespace MobWx.Lib.Models.Nws;

public class CloudLayer
{
    [JsonPropertyName("base")]
    public QuantitativeValue? CloudBaseM { get; set; }

    [JsonPropertyName("amount")]
    public Amount? Amount { get; set; }

    public override string ToString()
    {
        string result = string.Empty;
        result += Amount is null
            ? "unk"
            : Amount.ToString();
        result += ", ";
        result += CloudBaseM is null
            ? "unk"
            : CloudBaseM.Value is null
                ? "unk"
                : CloudBaseM.Value.ToString();
        return result;
    }
}
