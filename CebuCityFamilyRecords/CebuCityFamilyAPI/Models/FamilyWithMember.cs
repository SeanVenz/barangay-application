namespace CebuCityFamilyAPI.Models
{
    public class FamilyWithMember
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Sitio { get; set; }
        public int BId { get; set; }
        public Barangay? Barangay { get; set; }
        public List<FamilyMemberWithDetails> FamilyMembers { get; set; } = new List<FamilyMemberWithDetails>();
    }
}
