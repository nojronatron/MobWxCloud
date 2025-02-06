using System.Text.Json.Serialization;

namespace MobWx.Lib.NwsAlertModels
{

    public class ActiveAlerts
    {
        [JsonPropertyName("title")]
        public string? AlertTitle { get; set; } // alert sub-headline for UI display

        [JsonPropertyName("updated")]
        public DateTime? AlertUpdateTimestamp { get; set; } // date-time of last update to the Alert

        [JsonPropertyName("@graph")]
        public List<ActiveAlert> Graph { get; set; } = []; // list of ActiveAlert objects

        /// <summary>
        /// Convenience property returns count of active alerts in a graph.
        /// </summary>
        public int Count => Graph.Count;
    }
}
