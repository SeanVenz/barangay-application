using CebuCityFamilyAPI.Models;

namespace CebuCityFamilyAPI.Repositories.BarangayRepository
{
    public interface IBarangayRepository
    {
        /// <summary>
        /// A method that returns all barangays
        /// </summary>
        /// <returns>
        /// An IEnumerable of Barangay entities
        /// </returns>
        public Task<IEnumerable<Barangay>> GetAllBarangays();

        /// <summary>
        /// A method that returns a barangay with a specific value of <paramref name="id"/>
        /// </summary>
        /// <param name="id">
        /// Represents the Id or Primary Key of a barangay
        /// </param>
        /// <returns>
        /// A Barangay entity
        /// </returns>
        public Task<Barangay> GetBarangayById(int id);

        /// <summary>
        /// A method that returns a barangay with the name <paramref name="name"/>
        /// </summary>
        /// <param name="name">
        /// Represents the Name of a barangay
        /// </param>
        /// <returns>
        /// A Barangay entity
        /// </returns>
        public Task<Barangay> GetBarangayByName(string name);

        /// <summary>
        /// A method that returns the population of a barangay with the Id <paramref name="barangayId"/>
        /// </summary>
        /// <param name="barangayId"></param>
        /// <returns></returns>
        public Task<int> GetPopulationInBarangay(int barangayId);

        /// <summary>
        /// A method that creates a Barangay entity
        /// </summary>
        /// <param name="barangay">
        /// Represents all the fields needed to create the entity
        /// </param>
        /// <returns>
        /// An integer which represents the Id or Primary Key of the newly created entity
        /// </returns>
        public Task<int> CreateBarangay(Barangay barangay);

        /// <summary>
        /// A method that updates the Name and Captain of <paramref name="barangay"/>
        /// </summary>
        /// <param name="barangay">
        /// Represents all the fields needed to be updated in the entity.
        /// </param>
        /// <returns>
        /// The new state of the Barangay entity after updating
        /// </returns>
        public Task<Barangay> UpdateBarangay(Barangay barangay);

        public Task<Barangay> UpdateBarangayByName(string oldName, Barangay barangay);

        /// <summary>
        /// A method that deletes a Barangay entity with the name <paramref name="name"/>
        /// </summary>
        /// <param name="name">
        /// Represents the Name of a barangay
        /// </param>
        /// <returns>
        /// An integer representing the number of rows affected
        /// </returns>
        public Task<int> DeleteBarangay(string name);

        /// <summary>
        /// A method that counts the number of specific barangays with the name <paramref name="name"/>
        /// </summary>
        /// <param name="name">
        /// Represents the Name of a barangay
        /// </param>
        /// <returns>
        /// An integer representing the count of specific barangays with the name <paramref name="name"/>
        /// </returns>
        public Task<int> CountBarangayName(string name);
    }
}
