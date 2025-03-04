using System.ComponentModel.DataAnnotations;

namespace MobWx.Lib.Models.Forms;

public class CityStateModel
{
    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 2)]
    public string City { get; set; } = string.Empty;

    [Required]
    [StringLength(maximumLength: 3, MinimumLength = 2)]
    public string State { get; set; } = string.Empty;
}
