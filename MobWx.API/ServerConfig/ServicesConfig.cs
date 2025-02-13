using MobWx.API.Common;
using MobWx.API.Endpoints;

namespace MobWx.API.ServerConfig
{
    public static class ServicesConfig
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICurrentConditionsHandler, CurrentConditionsHandler>();
            builder.Services.AddTransient<INwsEndpointAbstraction, NwsEndpointAbstraction>();
            builder.Services.AddTransient<IJsonHandler, JsonHandler>();
            builder.Services.AddTransient<IForecastsHandler, ForecastsHandler>();
            builder.Services.AddTransient<IAlertsHandler, AlertsHandler>();
            builder.Services.AddTransient<ILocationHandler, LocationHandler>();
            builder.Services.AddTransient<IOpenStreetMapsAbstraction, OpenStreetMapsAbstraction>();
        }
    }
}
