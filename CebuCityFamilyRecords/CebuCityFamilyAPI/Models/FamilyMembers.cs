namespace CebuCityFamilyAPI.Models
{
    public class FamilyMembers
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int FId { get; set; }
        public List<Details> Detail { get; set; } = new List<Details>();
        public Details? Details { get; set; }
        public Family? Family { get; set; }
    }
}
