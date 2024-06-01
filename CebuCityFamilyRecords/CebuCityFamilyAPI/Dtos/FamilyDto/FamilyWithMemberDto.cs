using CebuCityFamilyAPI.Dtos.FamilyMembersDto;

namespace CebuCityFamilyAPI.Dtos.FamilyDto
{
    public class FamilyWithMemberDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Sitio { get; set; }
        public List<FamilyMemberWithDetailsDto> FamilyMembers { get; set; } = new List<FamilyMemberWithDetailsDto>();
    }
}
