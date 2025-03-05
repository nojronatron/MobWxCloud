using MobWx.Lib.Models;

namespace MobWx.API.Common;

public static class NwsEndpointPaths
{
    /// <summary>
    /// Get the relative path for the latest observation.
    /// </summary>
    /// <param name="stationId"></param>
    /// <returns></returns>
    public static string LatestObsPath(string stationId)
    {
        return $"/stations/{stationId}/observations/latest";
    }

    /// <summary>
    /// Get the relative path for a point.
    /// </summary>
    /// <param name="latLon"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public static string PointPath(Position latLon)
    {
        if (latLon.HasCoordinates)
        {
            return $"/points/{latLon.Coordinate!.Lat},{latLon.Coordinate.Lon}";
        }
        
        return string.Empty;
    }

    /// <summary>
    /// Get the relative path for a point. Limit min: 1, max: 25, default: 12.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="limit"></param>
    /// <returns>Remote API path (or empty string in method params were not valid)</returns>
    public static string GetActiveAlertPath(Position position, int? limit)
    {
        if (position.HasCoordinates)
        {
            int limitNum = 12;

            if (limit is not null
                && limit > 0
                && limit <= 25
                )
            {
                limitNum = (int)limit;
            }
        
            return $"/alerts/active?point={position.Coordinate!.Lat},{position.Coordinate.Lon}&limit={limitNum}";
        }
     
        return string.Empty;
    }

    /// <summary>
    /// Accepts a Position object and returns the path for the active alerts endpoint.
    /// Limits results to maximum of 12.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static string GetActiveAlertPath(Position position)
    {
        return GetActiveAlertPath(position, null);
    }
}
