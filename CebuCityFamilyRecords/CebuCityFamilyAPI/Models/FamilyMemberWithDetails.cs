namespace CebuCityFamilyAPI.Models
{
    public class FamilyMemberWithDetails
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public int Age { get; set; }
        public string? MaritalStatus { get; set; }
        public string? BirthDate { get; set; }
        public string? Gender { get; set; }
        public string? Occupation { get; set; }
        public string? ContactNo { get; set; }
        public string? Religion { get; set; }
        public int FId { get; set; }
    }

}
