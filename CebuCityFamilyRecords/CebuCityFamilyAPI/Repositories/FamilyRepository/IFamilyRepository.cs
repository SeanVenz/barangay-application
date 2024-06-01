using CebuCityFamilyAPI.Models;

namespace CebuCityFamilyAPI.Repositories.FamilyRepository
{
    public interface IFamilyRepository
    {
        /// <summary>
        /// Gets all Families from the Database
        /// </summary>
        /// <returns>An IEnumerable of all Families</returns>
        public Task<IEnumerable<Family>> GetAllFamilies();

        /// <summary>
        /// Gets a Family from the Database using a specific value of Barangay <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Integer Id representing a Barangay</param>
        /// <returns>A Family specified by their Barangay <paramref name="id"/></returns>
        public Task<IEnumerable<Family>> GetFamilyByBarangayId(int id);

        /// <summary>
        /// Gets a Family from the Database using a specific Family <paramref name="name"/>.
        /// </summary>
        /// <param name="name">A String representing the Family Name</param>
        /// <returns>A Family specified by their Family <paramref name="name"/> </returns>
        public Task<IEnumerable<Family>> GetFamilyByBarangayName(string name);

        /// <summary>
        /// Creates a Family in the Database
        /// </summary>
        /// <param name="family">The values needed to create the entity</param>
        /// <returns>The ID of the newly created Family</returns>
        public Task<int> CreateFamily(Family family);

        /// <summary>
        /// Gets a Family from the Database using a specific value of <paramref name="id"/>.
        /// </summary>
        /// <param name="id">Integer Id representing Primary Key of a Family</param>
        /// <returns>A Family specified by their Primary Key <paramref name="id"/> </returns>
        public Task<Family> GetFamilyById(int id);

        /// <summary>
        /// Updates an existing Family in the Database
        /// </summary>
        /// <param name="family">The values needed to update the entity</param>
        /// <returns>The Updated Family</returns>
        public Task<Family> UpdateFamily(Family family);

        /// <summary>
        /// Deletes an existing Family in the Database using their Family <paramref name="id"/>
        /// </summary>
        /// <param name="id">Integer Id representing Primary Key of a Family</param>
        /// <returns>The number of row(s) affected in the Database</returns>
        public Task<int> DeleteFamilyById(int id);

        /// <summary>
        /// Gets the family with their family members
        /// </summary>
        /// <returns>IEnumerable of family together with their members</returns>
        public Task<IEnumerable<FamilyWithMember>> GetFamilyWithFamilyMembers();

        /// <summary>
        /// Gets the family with their family members by ID
        /// </summary>
        /// <param name="id">ID of the family</param>
        /// <returns>A family with their family members</returns>
        public Task<FamilyWithMember> GetFamilyWithFamilyMembersById(int id);

        /// <summary>
        /// Gets the family with their family members by Name
        /// </summary>
        /// <param name="name">Name of the family</param>
        /// <returns>A family with their family members</returns>
        public Task<FamilyWithMember> GetFamilyWithFamilyMembersByName(string name);
    }
}
