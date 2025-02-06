using MobWx.Lib.Models;
using MobWx.Lib.Models.Base;
using MobWx.Lib.NwsAlertModels;
using System.Text.Json;

namespace MobWx.API.Endpoints
{
    static class AlertsHandlers
    {
        private static readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        /// <summary>
        /// Accepts a Position object and returns the path for the active alerts endpoint.
        /// Limited to a maximum of 12 alerts.
        /// </summary>
        /// <param name="position"></param>
        /// <returns>A string path for calling NWS API ActiveAlerts endpoint</returns>
        public static string GetActiveAlertPath(Position position)
        {
            int limit = 12;
            return $"/alerts/active?point={position.Latitude},{position.Longitude}&limit={limit}";
        }

        /// <summary>
        /// Fetches active alerts from the NWS API for a given position. Awaitable.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="httpRequest"></param>
        /// <param name="position"></param>
        /// <returns>Awaitable IResult with HTTP Status Code and Alert data or empty.</returns>
        public static async Task<IResult> GetActiveAlertsAsync(float lat, float lon, WebApplication app)
        {
            var position = PositionBase.Create(lat.ToString(), lon.ToString());

            if (position is NullPosition)
            {
                return Results.BadRequest("Invalid latitude (lat) or langitude (lon) values.");
            }

            using IServiceScope scope = app.Services.CreateScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();

                logger.LogInformation("Fetching alerts for (lat, lon): ({position}).", position);

                var httpClient = httpClientFactory.CreateClient("NwsApi");
                var request = new HttpRequestMessage(
                    HttpMethod.Get, GetActiveAlertPath((Position)position)
                    );
                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string activeAlertJson = await response.Content.ReadAsStringAsync();

                    logger.LogInformation("Received active alerts JSON: {activeAlertJson}", activeAlertJson);

                    ActiveAlerts? activeAlerts = JsonSerializer.Deserialize<ActiveAlerts>(activeAlertJson, jsonOptions);

                    if (activeAlerts is not null)
                    {
                        string alertCount = activeAlerts.Count.ToString();
                        string alerts = activeAlerts.Count == 1
                            ? "alert"
                            : "alerts";
                        logger.LogInformation("Deserialized {alertCount} active {alerts}: {activeAlerts}", alertCount, alerts, activeAlerts);
                    }
                    else
                    {
                        logger.LogWarning("Failed to deserialize any active alerts for location ({location}).", position.ToString());
                    }

                    return Results.Ok(activeAlerts);
                }
                else
                {
                    logger.LogError("Failed to get point data for {positionlatitude}, {positionlongitude}", position.Latitude, position.Longitude);
                    logger.LogWarning("Unable to process data from the NWS NOAA API. Try again later.");
                }

                return Results.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError("An error occurred while fetching alerts for lat: {positionGetLatitude}, lon: {positionGetLongitude}",
                    position.Latitude,
                    position.Longitude);
                logger.LogError("Stack Trace: {exStackTrace}", ex.StackTrace);
                return Results.Problem();
            }
        }
    }
}
