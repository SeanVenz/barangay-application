using System.ComponentModel.DataAnnotations;

namespace CebuCityFamilyAPI.Dtos.BarangayDto
{
    public class BarangayCreationDto
    {
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(100, ErrorMessage = "The maximum length for the Barangay Name is 50 characters")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Captain is Required")]
        [MaxLength(100, ErrorMessage = "The maximum length for the Captain Name is 100 characters")]
        public string? Captain { get; set; }
    }
}
