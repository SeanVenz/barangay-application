using System.ComponentModel.DataAnnotations;

namespace CebuCityFamilyAPI.Dtos.AccountDto
{
    public class AccountCreationDto
    {
        [Required(ErrorMessage = "First Name is Required!")]
        [MaxLength(100, ErrorMessage = "The maximum length for the Last Name is 100 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [MaxLength(100, ErrorMessage = "The maximum length for the First Name is 100 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is Required!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!")]
        [MaxLength(100, ErrorMessage = "The maximum length for the Email is 100 characters")]
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Phone Number is Required!")]
        [RegularExpression(@"^(\+63|0)[0-9]{10}$", ErrorMessage = "Invalid Phone Number!")]
        [MaxLength(13, ErrorMessage = "The maximum length for the Phone Number is 13 characters")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Government ID is required!")]
        [MaxLength(100, ErrorMessage = "The maximum length for the Government ID is 20 characters")]
        public string? GovernmentIdNumber { get; set; }

        [Required(ErrorMessage = "Password is Required!")]
        [MinLength(8, ErrorMessage = "The minimum length for the Password is 8 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&.,_-])[A-Za-z\d@$!%*?&.,_-]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string? Password { get; set; }
    }
}
