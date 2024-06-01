namespace CebuCityFamilyAPI.Models
{
    public class Barangay
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Captain { get; set; }
        public List<Family>? Families { get; set; } = new List<Family>();
    }
}
