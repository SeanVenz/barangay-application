using System.ComponentModel.DataAnnotations;

namespace CebuCityFamilyAPI.Dtos.FamilyMembersDto
{
    public class FamilyMemberWithDetailsCreationDto
    {
        [Required(ErrorMessage = "Last Name is required!")]
        [MaxLength(100, ErrorMessage = "The maximum length for the Last Name is 100 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "First Name is required!")]
        [MaxLength(100, ErrorMessage = "The maximum length for the First Name is 100 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Age is required!")]
        [Range(1, 150, ErrorMessage = "Please enter a valid age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Marital Status is required!")]
        public string? MaritalStatus { get; set; }

        [Required(ErrorMessage = "Birth Date is required!")]
        public string? BirthDate { get; set; }

        [Required(ErrorMessage = "Gender is required!")]
        public string? Gender { get; set; }

        public string? Occupation { get; set; }

        [RegularExpression(@"^(\+63|0)[0-9]{10}$", ErrorMessage = "Invalid Phone Number!")]
        [MaxLength(13, ErrorMessage = "The maximum length for the Contact Number is 13 characters")]
        public string? ContactNo { get; set; }

        public string? Religion { get; set; }

    }
}
