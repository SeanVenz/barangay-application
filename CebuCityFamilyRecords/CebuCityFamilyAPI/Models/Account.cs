namespace CebuCityFamilyAPI.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public string? GovernmentIdNumber { get; set; }
        public string? Password { get; set; }
    }
}
