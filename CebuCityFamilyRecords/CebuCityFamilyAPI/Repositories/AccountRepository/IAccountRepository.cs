using CebuCityFamilyAPI.Models;

namespace CebuCityFamilyAPI.Repositories.AccountRepository
{
    public interface IAccountRepository
    {
        /// <summary>
        /// A method that returns all the accounts
        /// </summary>
        /// <returns>
        /// IEnumerable of Account Entity
        /// </returns>
        public Task<IEnumerable<Account>> GetAllAccounts();

        /// <summary>
        /// Creates an Account
        /// </summary>
        /// <param name="account">Account details</param>
        /// <returns>
        /// Newly created account
        /// </returns>
        public Task<int> CreateAccount(Account account);

        /// <summary>
        /// Gets an account by Id
        /// </summary>
        /// <param name="id">Id of the account</param>
        /// <returns>
        /// The account with the given Id
        /// </returns>
        public Task<Account> GetAccountById(int id);

        /// <summary>
        /// A method that logs in an account
        /// </summary>
        /// <param name="emailAddress">Email of the user</param>
        /// <param name="password">Password of the user</param>
        /// <returns>
        /// Success message if found and error message if not found
        /// </returns>
        public Task<int?> Login(string emailAddress, string password);

        /// <summary>
        /// Deletes an account
        /// </summary>
        /// <param name="id">ID of the account</param>
        /// <returns>
        /// Success message if found and error message if not found
        /// </returns>
        public Task<bool> DeleteAccount(int id);
    }
}
