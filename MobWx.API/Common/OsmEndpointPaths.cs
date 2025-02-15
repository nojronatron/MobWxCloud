namespace MobWx.API.Common
{
    public static class OsmEndpointPaths
    {
        public static string NominatimBaseUrl => "https://nominatim.openstreetmap.org";
        public static string DefaultCountry => "country=United States";
        public static string DefaultFormat => "format=geojson";
        public static string DefaultLimit => "limit=1";

        public static string GetGeocodeRequestUrl(string city, string state)
        {
            string cityQ = $"city={city.Trim()}";
            string stateQ = $"state={state.Trim()}";
            return $"{NominatimBaseUrl}/search?{cityQ}&{stateQ}&{DefaultCountry}&{DefaultFormat}&{DefaultLimit}";
        }

        public static string GetGeocodeStatusUrl()
        {
            return $"{NominatimBaseUrl}/status";
        }
    }
}
