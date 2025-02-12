using MobWx.Lib.Models.Base;
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
    public static string PointPath(PositionBase latLon)
    {
        // if latLon is of type NullPosition return an empty string
        if (latLon is NullPosition)
        {
            return string.Empty;
        }
        
        return $"/points/{latLon.Latitude},{latLon.Longitude}";
    }

    /// <summary>
    /// Get the relative path for a point. Limit min: 1, max: 25, default: 12.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public static string GetActiveAlertPath(Position position, int? limit)
    {
        int limitNum = 12;

        if (limit is not null)
        {
            if (limit > 0 && limit <= 25)
            {
                limitNum = (int)limit;
            }
        }
     
        return $"/alerts/active?point={position.Latitude},{position.Longitude}&limit={limitNum}";
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
