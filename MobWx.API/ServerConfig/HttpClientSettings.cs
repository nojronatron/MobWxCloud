namespace MobWx.API.ServerConfig
{
    public static class HttpClientSettings
    {
        public static void ConfigureHttpClients(WebApplicationBuilder builder)
        {
            // common httpclient config
            string httpUserAgent = builder.Configuration["HttpClient:UserAgentHeader"] ?? string.Empty;
            string httpCtsTimeout = builder.Configuration["HttpClient:CancelTokenTimeout"] ?? "2000";

            // nws httpclient config
            string httpClientBaseNwsUrl = builder.Configuration["HttpClient:WeatherApiAddress"] ?? "localhost";
            string httpAcceptHeaderNws = builder.Configuration["HttpClient:AcceptHeaderNws"] ?? string.Empty;

            // osm httpclient config
            string httpClientBaseOsmUrl = builder.Configuration["HttpClient:OpenStreetMapsApiAddress"] ?? string.Empty;

            // http client originates specific endpoint calls to NWS API
            builder.Services.AddHttpClient("NwsApi", config =>
            {
                config.BaseAddress = new Uri(httpClientBaseNwsUrl);
                config.DefaultRequestHeaders.Add("Accept", httpAcceptHeaderNws);
                config.DefaultRequestHeaders.Add("User-Agent", httpUserAgent);
                config.Timeout = TimeSpan.FromMilliseconds(int.Parse(httpCtsTimeout));
            });

            // http client fetches data from extracted NWS urls
            builder.Services.AddHttpClient("NwsElementUrl", config =>
            {
                config.DefaultRequestHeaders.Add("Accept", httpAcceptHeaderNws);
                config.DefaultRequestHeaders.Add("User-Agent", httpUserAgent);
                config.Timeout = TimeSpan.FromMilliseconds(int.Parse(httpCtsTimeout));
            });

            // http client originates specific endpoing calls to OpenStreetMaps API
            builder.Services.AddHttpClient("OsmApi", config =>
            {
                config.BaseAddress = new Uri("https://nominatim.openstreetmap.org/");
                config.DefaultRequestHeaders.Add("User-Agent", httpUserAgent);
                config.Timeout = TimeSpan.FromMilliseconds(int.Parse(httpCtsTimeout));
            });
        }
    }
}
