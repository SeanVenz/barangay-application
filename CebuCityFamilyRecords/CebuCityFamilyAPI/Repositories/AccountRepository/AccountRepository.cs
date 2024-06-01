using CebuCityFamilyAPI.Context;
using CebuCityFamilyAPI.Models;
using Dapper;
using System.Data;

namespace CebuCityFamilyAPI.Repositories.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DapperContext _context;

        public AccountRepository(DapperContext con)
        {
            _context = con;
        }
        public async Task<int> CreateAccount(Account account)
        {
            var storedProc = "[spAccount_CreateAccount_WithValidation]";

            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", account.FirstName, DbType.String, size: 50);
                parameters.Add("@LastName", account.LastName, DbType.String, size: 50);
                parameters.Add("@EmailAddress", account.EmailAddress, DbType.String, size: 50);
                parameters.Add("@PhoneNumber", account.PhoneNumber, DbType.String, size: 50);
                parameters.Add("@GovernmentIdNumber", account.GovernmentIdNumber, DbType.String, size: 50);
                parameters.Add("@Password", account.Password, DbType.String, size: 50);
                parameters.Add("@ErrorCode", DbType.Int32, direction: ParameterDirection.Output);

                var newAccountId = await connection.ExecuteScalarAsync<int>(storedProc, parameters, commandType: CommandType.StoredProcedure);

                var errorCode = parameters.Get<int>("@ErrorCode");

                if (errorCode != 0)
                {
                    newAccountId = -1 * errorCode;
                }

                return newAccountId;
            }
        }

        public async Task<Account> GetAccountById(int id)
        {
            var sql = "SELECT * FROM Account WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Account>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            var sql = "SELECT * FROM Account";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Account>(sql);
            }
        }

        public async Task<int?> Login(string emailAddress, string password)
        {
            var storedProc = "[spAccount_Login]";

            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@EmailAddress", emailAddress, DbType.String, size: 50);
                parameters.Add("@Password", password, DbType.String, size: 50);
                parameters.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@ErrorCode", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(storedProc, parameters, commandType: CommandType.StoredProcedure);

                var errorCode = parameters.Get<int>("@ErrorCode");

                if (errorCode != 0)
                {
                    return null;
                }

                return parameters.Get<int>("@UserId");
            }
        }

        public async Task<bool> DeleteAccount(int id)
        {
            var sql = "DELETE FROM Account WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(sql, new { Id = id }) > 0;
            }
        }
    }
}
