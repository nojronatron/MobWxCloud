using System.Text.Json.Serialization;

namespace MobWx.Lib.NwsAlertModels
{
    public class ActiveAlert
    {
        [JsonPropertyName("@id")]
        public string? Id { get; set; } // unique id of this Alert content

        [JsonPropertyName("areaDesc")]
        public string? AreaDescription { get; set; } // description of the area affected by this Alert

        [JsonPropertyName("affectedZones")]
        public List<string>? AffectedZones { get; set; } // list of zones affected by this Alert

        [JsonPropertyName("sent")]
        public DateTime? Sent { get; set; } // date and time of Alert issuance

        [JsonPropertyName("effective")]
        public DateTime? Effective { get; set; } // date and time of start of Alert

        [JsonPropertyName("onset")]
        public DateTime? Onset { get; set; } // date and time of start of the EVENT

        [JsonPropertyName("expires")]
        public DateTime? Expires { get; set; } // date and time of expiration of the Alert

        [JsonPropertyName("ends")]
        public DateTime? Ends { get; set; } // anticipated date-time of the end of the EVENT

        [JsonPropertyName("status")]
        public string? Status { get; set; } // actual status of the Alert (actual, exercise, system)

        [JsonPropertyName("messageType")]
        public string? MessageType { get; set; } // type of message (Alert, Update, Cancel, Expired)

        [JsonPropertyName("severity")]
        public string? Severity { get; set; } // severity of the event (minor, moderate, severe)

        [JsonPropertyName("certainty")]
        public string? Certainty { get; set; } // certainty of the event (observed, likely, possible)

        [JsonPropertyName("event")]
        public string? EventSubject { get; set; } // the subject of the event

        [JsonPropertyName("senderName")]
        public string? NwsOffice { get; set; } // the NWS Office publishing the Alert

        [JsonPropertyName("headline")]
        public string? Headline { get; set; } // headline of the Alert for UI display

        [JsonPropertyName("description")]
        public string? Description { get; set; } // description of the Alert

        [JsonPropertyName("instruction")]
        public string? Instruction { get; set; } // instructions for people and organizations within the area of the Alert

        [JsonPropertyName("response")]
        public string? Response { get; set; } // instructions for those in the area of the Alert
    }
}
