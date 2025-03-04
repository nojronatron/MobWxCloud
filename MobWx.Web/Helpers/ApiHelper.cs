using MobWx.Lib.Models.Forms;
using MobWx.Lib.Models.Geocoding;

namespace MobWx.Web.Helpers;

public class ApiHelper(HttpClient httpClient)
{
    public async Task<Location?> GetGeolocationAsync(
        CityStateModel cityState,
        CancellationToken cancellationToken = default
        )
    {
        var response = await httpClient.GetFromJsonAsync<Location>($"api/v1/location/{cityState.City},{cityState.State}");
        return response;
    }
}
