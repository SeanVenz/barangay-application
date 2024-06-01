using CebuCityFamilyAPI.Models;

namespace CebuCityFamilyAPI.Repositories.FamilyMembersRepository
{
    public interface IFamilyMembersRepository
    {

        /// <summary>
        /// Gets a FamilyMember from the Database using a specific value of <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Integer Id representing a FamilyMember</param>
        /// <returns>A FamilyMember specified by <paramref name="id"/></returns>
        public Task<IEnumerable<FamilyMembers>> GetFamilyMembersById(int id);

        /// <summary>
        /// Gets a FamilyMember from the Database using a specific string of <paramref name="name"/>.
        /// </summary>
        /// <param name="name">A String representing the FamilyMember Name</param>
        /// <returns>A FamilyMember specified by <paramref name="name"/> </returns>
        public Task<IEnumerable<FamilyMembers>> GetFamilyMembersByName(string name);

        /// <summary>
        /// Creates a FamilyMember in the Database
        /// </summary>
        /// <param name="familyMembers">The values needed to create the entity</param>
        /// <returns>The ID of the newly created FamilyMember</returns>
        public Task<int> CreateFamilyMembers(FamilyMembers familyMembers);

        /// <summary>
        /// Gets all the family member with details
        /// </summary>
        /// <returns>
        /// IEnumerable of Family members
        /// </returns>
        public Task<IEnumerable<FamilyMemberWithDetails>> GetAllFamilyMemberWithDetails();

        /// <summary>
        /// Gets a family member with details by their id
        /// </summary>
        /// <param name="id">ID of the family member</param>
        /// <returns>
        /// The family member with corresponding id
        /// </returns>

        public Task<FamilyMemberWithDetails> GetFamilyMemberWithDetailsById(int id);

        /// <summary>
        /// Gets a family members with details by their last name
        /// </summary>
        /// <param name="name">Name of the family member</param>
        /// <returns>
        /// A single or list of family members
        /// </returns>
        public Task<IEnumerable<FamilyMemberWithDetails>> GetFamilyMemberWithDetailsByName(string name);

        /// <summary>
        /// Registers a family member with details to a family
        /// </summary>
        /// <param name="id">ID of the family</param>
        /// <param name="familyMemberWithDetails">Details of the family member</param>
        /// <returns>
        /// Newly created family member
        /// </returns>
        public Task<int> CreateFamilyMemberWithDetails(int id, FamilyMemberWithDetails familyMemberWithDetails);

        /// <summary>
        /// Updates a family member and their details
        /// </summary>
        /// <param name="familyWithMember">New family member details</param>
        /// <returns>
        /// Newly updated family member
        /// </returns>
        public Task<bool> UpdateFamilyMemberWithDetails(FamilyMemberWithDetails familyWithMember);

        /// <summary>
        /// Deletes a family member and their details using their id
        /// </summary>
        /// <param name="id">ID of the family member</param>
        /// <returns>
        /// Success if deleted
        /// </returns>
        public Task<bool> DeleteFamilyMemberWithDetails(int id);

    }
}
