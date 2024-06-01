using System.ComponentModel.DataAnnotations;

namespace CebuCityFamilyAPI.Dtos.FamilyDto
{
    public class FamilyCreationDto
    {
        [Required(ErrorMessage = "Name  is Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Sitio  is Required")]
        public string? Sitio { get; set; }
    }
}
