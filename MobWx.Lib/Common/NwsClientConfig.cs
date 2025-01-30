using MobWx.Lib.Models;

namespace MobWx.Lib.Common
{
    public class NwsClientConfig
    {
        public static string GridpointIdXYPattern => @"^\?[a-zA-Z]{3},\d{1,3},\d{1,3}$";

        private NwsClientConfig() { }

        public static string FoRelativePath(string gridId, string gridX, string gridY)
        {
            return $"/gridpoints/{gridId.ToUpper()}/{gridX},{gridY}/stations";
        }

        public static string LatestObsPath(string stationId)
        {
            return $"/stations/{stationId}/observations/latest";
        }

        public static string PointPath(PositionBase latLon)
        {
            // if latLon is of type NullPosition return an empty string
            if (latLon is NullPosition)
            {
                return string.Empty;
            }
            else
            {
                return $"/points/{latLon.GetLatitude()},{latLon.GetLongitude()}";
            }
        }
    }
}
