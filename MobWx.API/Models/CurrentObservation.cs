using MobWx.Lib.Models;

namespace MobWx.API.Models;

public partial class CurrentObservation
{
    public string? Location { get; set; }
    public double? StationElevation { get; set; }
    public string Station { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string RawMessage { get; set; } = string.Empty; // could be METAR
    public string Description { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public double? TemperatureC { get; set; }
    public int? TemperatureF { get; set; }
    public double? DewpointC { get; set; }
    public int? DewpointF { get; set; }
    public int? WindDirection { get; set; }
    public double? WindSpeedKph { get; set; }
    public int? WindSpeedMph { get; set; }
    public double? WindGustKph { get; set; }
    public int? WindGustMph { get; set; }
    public double? PressureMb { get; set; } // whole millimeters as hundreths of Pascals
    public double? PressureIn { get; set; } // proportianal inches to the tenths
    public int? VisibilityMeters { get; set; }
    public int? VisibilityMiles { get; set; }
    public double? MaxTemperatureC { get; set; } // 24hr period
    public int? MaxTemperatureF { get; set; } // 24hr period
    public double? MinTemperatureC { get; set; } // 24hr period
    public int? MinTemperatureF { get; set; } // 24hr period
    public int? PrecipitationMmHr { get; set; } // 1hr period
    public double? PrecipitationInchHr { get; set; } // 1hr period
    public double? WindChillC { get; set; }
    public int? WindChillF { get; set; }
    public double? HeatIndexC { get; set; }
    public int? HeatIndexF { get; set; }
    public List<CloudLayer>? CloudLayers { get; set; }
}
