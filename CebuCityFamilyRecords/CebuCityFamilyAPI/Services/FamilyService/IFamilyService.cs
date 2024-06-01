using CebuCityFamilyAPI.Dtos.FamilyDto;

namespace CebuCityFamilyAPI.Services.FamilyService
{
    public interface IFamilyService
    {
        /// <summary>
        /// Gets all Families
        /// </summary>
        /// <returns>All Families</returns>
        public Task<IEnumerable<FamilyDto>> GetAllFamilies();

        /// <summary>
        /// A method that registers a Family to a Barangay
        /// </summary>
        /// <param name="BId">
        /// Represents the Barangay Id of the newly registered Family
        /// </param>
        /// <param name="family">
        /// Represents the fields needed to register a Family
        /// </param>
        /// <returns>
        /// The Id or Primary Key of the newly registered Family
        /// </returns>
        public Task<int> Register(int BId, FamilyCreationDto family);

        /// <summary>
        /// Gets a Family using their specific Barangay <paramref name="id"/>
        /// </summary>
        /// <param name="id">Integer ID</param>
        /// <returns>A Family</returns>
        public Task<IEnumerable<FamilyDto>> GetFamilyByBarangayId(int id);

        /// <summary>
        /// Gets a Family using their specific <paramref name="id"/>
        /// </summary>
        /// <param name="id">Integer ID representing a Barangay</param>
        /// <returns>A Family</returns>
        public Task<FamilyDto?> GetFamilyById(int id);

        /// <summary>
        /// Gets a Family by their family <paramref name="name"/>
        /// </summary>
        /// <param name="name">The Name of the Family</param>
        /// <returns>A Family</returns>
        public Task<IEnumerable<FamilyDto>> GetFamilyByBarangayName(string name);

        /// <summary>
        /// Creates a Family
        /// </summary>
        /// <param name="familyToCreate">Family to be Created</param>
        /// <returns>The newly created Family</returns>
        public Task<FamilyDto> CreateFamily(FamilyCreationDto familyToCreate);

        /// <summary>
        /// Updates a Family
        /// </summary>
        /// <param name="id">Specific ID of the Family</param>
        /// <param name="familyToUpdate">Family to be Updated</param>
        /// <returns>The Updated Family</returns>
        public Task<FamilyDto> UpdateFamily(int id, FamilyUpdationDto familyToUpdate);

        /// <summary>
        /// Deletes a Family using their specific Family <paramref name="id"/>
        /// </summary>
        /// <param name="id">Specific ID of the Family</param>
        public Task DeleteFamilyById(int id);

        /// <summary>
        /// Gets all Family with their Members
        /// </summary>
        /// <returns>IEnumerable of Family and members</returns>
        public Task<IEnumerable<FamilyWithMemberDto>> GetFamilyWithFamilyMembers();

        /// <summary>
        /// Gets a Family with their Members using their specific Family
        /// </summary>
        /// <param name="id">ID of the family</param>
        /// <returns>amily with their Members</returns>
        public Task<FamilyWithMemberDto> GetFamilyWithFamilyMembersById(int id);

        /// <summary>
        /// Gets a Family with their Members using their specific name
        /// </summary>
        /// <param name="name">Name of the family</param>
        /// <returns>A Family</returns>
        public Task<FamilyWithMemberDto> GetFamilyWithFamilyMembersByName(string name);
    }
}
