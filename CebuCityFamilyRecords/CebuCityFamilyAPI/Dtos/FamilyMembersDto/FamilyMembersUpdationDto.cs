using System.ComponentModel.DataAnnotations;

namespace CebuCityFamilyAPI.Dtos.FamilyMembersDto
{
    public class FamilyMembersUpdationDto
    {
        [Required(ErrorMessage = "Name is Required")]
        public string? Name { get; set; }
    }
}
