using CebuCityFamilyAPI.Dtos.FamilyMembersDto;

namespace CebuCityFamilyAPI.Services.FamilyMembersService
{
    public interface IFamilyMembersService
    {

        /// <summary>
        /// Gets a FamilyMember using their specific <paramref name="id"/>
        /// </summary>
        /// <param name="id">Integer ID</param>
        /// <returns>A FamilyMember</returns>
        public Task<IEnumerable<FamilyMembersDto>> GetFamilyMembersById(int id);

        /// <summary>
        /// Gets a FamilyMember by their familyMember <paramref name="name"/>
        /// </summary>
        /// <param name="name"></param>
        /// <returns>A FamilyMember</returns>
        public Task<IEnumerable<FamilyMembersDto>> GetFamilyMembersByName(string name);

        /// <summary>
        /// Gets the family with members
        /// </summary>
        /// <returns>IEnumerable of family and their members</returns>
        public Task<IEnumerable<FamilyMemberWithDetailsDto>> GetAllFamilyMemberWithDetails();

        /// <summary>
        /// Gets a family with their members using their id
        /// </summary>
        /// <param name="id">id of the family</param>
        /// <returns>Family with their members</returns>
        public Task<FamilyMemberWithDetailsDto?> GetFamilyMemberWithDetailsById(int id);

        /// <summary>
        /// Gets a family with their member using their name
        /// </summary>
        /// <param name="name">Name of the family</param>
        /// <returns>Family with their members</returns>
        public Task<IEnumerable<FamilyMemberWithDetailsDto>> GetFamilyMemberWithDetailsByName(string name);

        /// <summary>
        /// Registers a family member to a family
        /// </summary>
        /// <param name="id">ID of the family</param>
        /// <param name="familyMember">Family Model</param>
        /// <returns>Newly created member</returns>
        public Task<FamilyMemberWithDetailsDto> CreateFamilyMemberWithDetails(int id, FamilyMemberWithDetailsCreationDto familyMember);

        /// <summary>
        /// Updates a family member
        /// </summary>
        /// <param name="id">ID of the member</param>
        /// <param name="familyMember">Member model</param>
        /// <returns>True if successful, False if otherwise</returns>
        public Task<bool> UpdateFamilyMemberWithDetails(int id, FamilyMemberWithDetailsUpdationDto familyMember);

        /// <summary>
        /// Deletes a family member
        /// </summary>
        /// <param name="id">ID of the family member</param>
        /// <returns>True if successful, False if otherwise</returns>
        public Task<bool> DeleteFamilyMemberWithDetails(int id);
    }
}