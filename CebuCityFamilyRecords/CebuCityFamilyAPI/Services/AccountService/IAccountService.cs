using CebuCityFamilyAPI.Dtos.AccountDto;

namespace CebuCityFamilyAPI.Services.AccountService
{
    public interface IAccountService
    {
        /// <summary>
        /// Gets all the accounts
        /// </summary>
        /// <returns>IEnumerable of AccountDtos</returns>
        public Task<IEnumerable<AccountDto>> GetAllAccount();

        /// <summary>
        /// Creates an account
        /// </summary>
        /// <param name="AccountDto">Account model</param>
        /// <returns>Newly created account</returns>
        public Task<AccountDto> CreateAccount(AccountCreationDto AccountDto);

        /// <summary>
        /// Gets an account by ID
        /// </summary>
        /// <param name="id">ID of the account</param>
        /// <returns>An Account</returns>
        public Task<AccountDto?> GetAccountById(int id);

        /// <summary>
        /// Logs in an account
        /// </summary>
        /// <param name="emailAddress">Email of the account</param>
        /// <param name="password">Password of the accoount</param>
        /// <returns></returns>
        public Task<int?> Login(string emailAddress, string password);

        /// <summary>
        /// Deletes an account by ID
        /// </summary>
        /// <param name="id">ID of the account</param>
        /// <returns>Success message</returns>
        public Task<bool> DeleteAccount(int id);
    }
}
