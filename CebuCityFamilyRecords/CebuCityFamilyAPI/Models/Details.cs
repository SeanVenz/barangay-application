using System.Text.Json.Serialization;

namespace CebuCityFamilyAPI.Models
{
    public class Details
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string? CivilStatus { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Occupation { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Religion { get; set; }
        [JsonIgnore]
        public int FmId { get; set; }
        public FamilyMembers? FamilyMembers { get; set; }
    }
}
