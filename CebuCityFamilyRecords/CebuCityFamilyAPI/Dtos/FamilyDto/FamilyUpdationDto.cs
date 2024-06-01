using System.ComponentModel.DataAnnotations;

namespace CebuCityFamilyAPI.Dtos.FamilyDto
{
    public class FamilyUpdationDto
    {
        [Required(ErrorMessage = "Sitio is Required")]
        public string? Sitio { get; set; }
    }
}
