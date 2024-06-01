using Microsoft.Data.SqlClient;
using System.Data;

namespace CebuCityFamilyAPI.Context
{
    public class DapperContext
    {
        private string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServer");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
