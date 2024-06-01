namespace CebuCityFamilyAPI.Models
{
    public class Family
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Sitio { get; set; }
        public int BId { get; set; }
        public Barangay? Barangay { get; set; }
    }
}
