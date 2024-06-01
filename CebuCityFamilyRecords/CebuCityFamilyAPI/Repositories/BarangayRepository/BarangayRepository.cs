using CebuCityFamilyAPI.Context;
using CebuCityFamilyAPI.Models;
using Dapper;
using System.Data;

namespace CebuCityFamilyAPI.Repositories.BarangayRepository
{
    public class BarangayRepository : IBarangayRepository
    {
        private readonly DapperContext _context;

        public BarangayRepository(DapperContext con)
        {
            _context = con;
        }

        public async Task<int> CreateBarangay(Barangay barangay)
        {
            var procedure = "[spBarangay_CreateBarangay]";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(procedure, new { barangay.Name, barangay.Captain }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Barangay>> GetAllBarangays()
        {
            var sql = "SELECT * FROM Barangay";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Barangay>(sql);
            }
        }

        public async Task<Barangay> GetBarangayById(int id)
        {
            var sql = "SELECT * FROM Barangay WHERE Id = @id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Barangay>(sql, new { id });
            }
        }

        public async Task<Barangay> GetBarangayByName(string name)
        {
            var sql = "SELECT * FROM Barangay as b WHERE b.Name = @name";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Barangay>(sql, new { name });
            }
        }

        public async Task<int> GetPopulationInBarangay(int barangayId)
        {
            var procedure = "[spBarangay_GetFamilyMembersCount]";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(procedure, new { barangayId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Barangay> UpdateBarangay(Barangay barangay)
        {
            var procedure = "[spBarangay_UpdateBarangay]";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleAsync<Barangay>(procedure, new { barangay.Name, barangay.Captain, barangay.Id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Barangay> UpdateBarangayByName(string oldName, Barangay barangay)
        {
            var procedure = "spBarangay_UpdateBarangayUsingName";

            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@oldName", oldName);
                parameters.Add("@newName", barangay.Name);
                parameters.Add("@newCaptain", barangay.Captain);

                return await connection.QuerySingleAsync<Barangay>(procedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> DeleteBarangay(string name)
        {
            var procedure = "[spBarangay_DeleteBarangay]";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(procedure, new { name }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> CountBarangayName(string name)
        {
            var sql = "SELECT COUNT(*) FROM Barangay AS b WHERE b.Name = @name";
            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, new { name });
            }
        }
    }
}
