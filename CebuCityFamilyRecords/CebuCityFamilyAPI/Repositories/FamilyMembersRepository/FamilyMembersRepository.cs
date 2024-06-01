using CebuCityFamilyAPI.Context;
using CebuCityFamilyAPI.Models;
using Dapper;
using System.Data;

namespace CebuCityFamilyAPI.Repositories.FamilyMembersRepository
{
    public class FamilyMembersRepository : IFamilyMembersRepository
    {
        private readonly DapperContext _context;

        public FamilyMembersRepository(DapperContext con)
        {
            _context = con;
        }

        public async Task<int> CreateFamilyMembers(FamilyMembers familyMembers)
        {
            var proc = "spFamilyMembers_CreateFamilyMembers";
            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(proc,
                    new
                    {
                        familyMembers.Name,
                        familyMembers.FId
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<FamilyMembers>> GetFamilyMembersById(int id)
        {
            var sql = "SELECT * FROM FamilyMembers as fm INNER JOIN Details as d ON fm.id = d.FmId WHERE fm.FId = @id;";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<FamilyMembers, Details, FamilyMembers>(sql, (fm, d) =>
                {
                    fm.Detail.Add(d);
                    return fm;
                }, new { id });
            }
        }

        public async Task<IEnumerable<FamilyMembers>> GetFamilyMembersByName(string name)
        {
            var proc = "spFamilyMembers_GetAllByFamilyName";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<FamilyMembers, Details, FamilyMembers>(proc, (fm, d) =>
                {
                    fm.Detail.Add(d);
                    return fm;
                }, new { name }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<FamilyMemberWithDetails>> GetAllFamilyMemberWithDetails()
        {
            var sql = "SELECT * FROM FamilyMemberWithDetails";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<FamilyMemberWithDetails>(sql);
            }
        }

        public async Task<FamilyMemberWithDetails> GetFamilyMemberWithDetailsById(int id)
        {
            var sql = "SELECT * FROM FamilyMemberWithDetails WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<FamilyMemberWithDetails>(sql, new { id });
            }
        }

        public async Task<int> CreateFamilyMemberWithDetails(int id, FamilyMemberWithDetails familyMemberWithDetails)
        {
            var proc = "[spMemberDetails_CreateMemberDetails]";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(proc, new
                {
                    familyMemberWithDetails.LastName,
                    familyMemberWithDetails.FirstName,
                    familyMemberWithDetails.Age,
                    familyMemberWithDetails.MaritalStatus,
                    familyMemberWithDetails.BirthDate,
                    familyMemberWithDetails.Gender,
                    familyMemberWithDetails.Occupation,
                    familyMemberWithDetails.ContactNo,
                    familyMemberWithDetails.Religion,
                    FId = id
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> UpdateFamilyMemberWithDetails(FamilyMemberWithDetails familyMember)
        {
            var storedProc = "[dbo].[spFamilyWithMember_UpdateMember]";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(storedProc, new
                {
                    familyMember.Id,
                    familyMember.LastName,
                    familyMember.FirstName,
                    familyMember.Age,
                    familyMember.MaritalStatus,
                    familyMember.BirthDate,
                    familyMember.Gender,
                    familyMember.Occupation,
                    familyMember.ContactNo,
                    familyMember.Religion
                }, commandType: CommandType.StoredProcedure) > 0;

            }
        }

        public async Task<IEnumerable<FamilyMemberWithDetails>> GetFamilyMemberWithDetailsByName(string name)
        {
            var sql = "SELECT * FROM FamilyMemberWithDetails WHERE LastName = @name";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<FamilyMemberWithDetails>(sql, new { name });
            }
        }

        public async Task<bool> DeleteFamilyMemberWithDetails(int id)
        {
            var sql = "DELETE FROM FamilyMemberWithDetails WHERE Id = @id";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteAsync(sql, new { id }) > 0;
            }
        }
    }
}
