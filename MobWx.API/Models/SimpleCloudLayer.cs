namespace MobWx.API.Models;

public class SimpleCloudLayer
{
    public string? HeightMeters { get; set; }
    public string? Description { get; set; }

    public string? HeightFeet
    {
        get
        {
            if (HeightMeters is not null
                && double.TryParse(HeightMeters, out double height))
            {
                return Math.Round(height * 3.28084).ToString();
            }

            return null;
        }
    }
}
