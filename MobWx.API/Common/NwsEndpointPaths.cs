using MobWx.Lib.Models.Base;
using MobWx.Lib.Models;

namespace MobWx.API.Common;

public static class NwsEndpointPaths
{
    /// <summary>
    /// Get the relative path for a gridpoint.
    /// </summary>
    /// <param name="gridId"></param>
    /// <param name="gridX"></param>
    /// <param name="gridY"></param>
    /// <returns></returns>
    public static string FoRelativePath(string gridId, string gridX, string gridY)
    {
        return $"/gridpoints/{gridId.ToUpper()}/{gridX},{gridY}/stations";
    }

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
    /// <returns></returns>
    public static string PointPath(PositionBase latLon)
    {
        // if latLon is of type NullPosition return an empty string
        if (latLon is NullPosition)
        {
            return string.Empty;
        }
        else
        {
            return $"/points/{latLon.Latitude},{latLon.Longitude}";
        }
    }
}
