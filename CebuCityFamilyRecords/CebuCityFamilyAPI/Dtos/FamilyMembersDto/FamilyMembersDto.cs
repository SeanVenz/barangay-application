namespace CebuCityFamilyAPI.Dtos.FamilyMembersDto
{
    public class FamilyMembersDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IEnumerable<FamilyMemberDetailsDto> Details { get; set; }
    }
}
