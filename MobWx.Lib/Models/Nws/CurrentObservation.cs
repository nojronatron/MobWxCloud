namespace MobWx.Lib.Models.Nws;

/// <summary>
/// Represent the current observation from a weather station, sent as a Json Response by the API.
/// </summary>
public partial class CurrentObservation
{
    public string? StationLocation { get; set; }
    public decimal? StationElevation { get; set; }
    public string StationUri { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string RawMessage { get; set; } = string.Empty; // could be METAR
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public decimal? TemperatureC { get; set; }
    public int? TemperatureF { get; set; }
    public decimal? DewpointC { get; set; }
    public int? DewpointF { get; set; }
    public int? WindDirection { get; set; }
    public decimal? WindSpeedKph { get; set; }
    public int? WindSpeedMph { get; set; }
    public decimal? WindGustKph { get; set; }
    public int? WindGustMph { get; set; }
    public decimal? PressureMb { get; set; } // whole millimeters as hundreths of Pascals
    public decimal? PressureIn { get; set; } // proportianal inches to the tenths
    public int? VisibilityMeters { get; set; }
    public int? VisibilityMiles { get; set; }
    public decimal? MaxTemperatureC { get; set; } // 24hr period
    public int? MaxTemperatureF { get; set; } // 24hr period
    public decimal? MinTemperatureC { get; set; } // 24hr period
    public int? MinTemperatureF { get; set; } // 24hr period
    public int? PrecipitationMmHr { get; set; } // 1hr period
    public decimal? PrecipitationInchHr { get; set; } // 1hr period
    public int? RelativeHumidity { get; set; } // percentage
    public decimal? WindChillC { get; set; }
    public int? WindChillF { get; set; }
    public decimal? HeatIndexC { get; set; }
    public int? HeatIndexF { get; set; }
    public List<SimpleCloudLayer>? CloudLayers { get; set; }
}
