using CebuCityFamilyAPI.Context;
using CebuCityFamilyAPI.Models;
using Dapper;
using System.Data;

namespace CebuCityFamilyAPI.Repositories.FamilyRepository
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly DapperContext _context;

        public FamilyRepository(DapperContext con)
        {
            _context = con;
        }

        public async Task<int> CreateFamily(Family family)
        {
            var proc = "spFamily_CreateFamily";

            using (var con = _context.CreateConnection())
            {
                return await con.ExecuteScalarAsync<int>(proc,
                    new
                    {
                        family.Name,
                        family.Sitio,
                        family.BId
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Family>> GetAllFamilies()
        {
            var sql = "SELECT Id, Name, Sitio FROM Family";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Family>(sql);
            }
        }

        public async Task<IEnumerable<Family>> GetFamilyByBarangayId(int id)
        {
            var sql = "SELECT * FROM Family as f WHERE f.BId = @id;";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Family>(sql, new { id });
            }
        }

        public async Task<Family> GetFamilyById(int id)
        {
            var sql = "SELECT * FROM Family WHERE Id = @id";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Family>(sql, new { id });
            }
        }

        public async Task<IEnumerable<Family>> GetFamilyByBarangayName(string name)
        {
            var proc = "spFamily_GetAllByBarangayName";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Family>(proc, new { name }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Family> UpdateFamily(Family family)
        {
            var proc = "spFamily_UpdateFamily";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleAsync<Family>(proc, new { family.Id, family.Sitio }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> DeleteFamilyById(int id)
        {
            var proc = "spFamily_DeleteFamilyById";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(proc, new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<FamilyWithMember>> GetFamilyWithFamilyMembers()
        {
            var sql = "SELECT * FROM Family as f INNER JOIN FamilyMemberWithDetails as fmd ON f.id = fmd.FId";

            using (var connection = _context.CreateConnection())
            {
                var family = await connection.QueryAsync<FamilyWithMember, FamilyMemberWithDetails, FamilyWithMember>(sql, (family, familyMemberWithDetails) =>
                {
                    family.FamilyMembers.Add(familyMemberWithDetails);
                    return family;
                });
                return family.GroupBy(f => f.Id).Select(fm =>
                {
                    var first = fm.First();
                    first.FamilyMembers = fm.SelectMany(f => f.FamilyMembers).ToList();
                    return first;
                });
            }
        }

        public async Task<FamilyWithMember> GetFamilyWithFamilyMembersById(int id)
        {
            var sql = "SELECT * FROM Family WHERE Id = @id; SELECT * FROM FamilyMemberWithDetails WHERE FId = @id";

            using (var connection = _context.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(sql, new { id }))
                {
                    var family = await multi.ReadSingleOrDefaultAsync<FamilyWithMember>();
                    var familyMembers = multi.Read<FamilyMemberWithDetails>().ToList();

                    if (family != null)
                    {
                        family.FamilyMembers = familyMembers;
                    }
                    return family;
                }
            }
        }

        public async Task<FamilyWithMember> GetFamilyWithFamilyMembersByName(string name)
        {
            var sql = @"SELECT * FROM Family as f 
                LEFT JOIN FamilyMemberWithDetails as fmd ON f.Id = fmd.FId 
                WHERE f.Name = @name";

            var familyDictionary = new Dictionary<int, FamilyWithMember>();

            using (var connection = _context.CreateConnection())
            {
                var list = await connection.QueryAsync<FamilyWithMember, FamilyMemberWithDetails, FamilyWithMember>(
                    sql,
                    (family, familyMemberWithDetails) =>
                    {
                        FamilyWithMember familyEntry;

                        if (!familyDictionary.TryGetValue(family.Id, out familyEntry))
                        {
                            familyEntry = family;
                            familyEntry.FamilyMembers = new List<FamilyMemberWithDetails>();
                            familyDictionary.Add(familyEntry.Id, familyEntry);
                        }

                        familyEntry.FamilyMembers.Add(familyMemberWithDetails);
                        return familyEntry;
                    },
                    param: new { name },
                    splitOn: "Id");

                return list.Distinct().FirstOrDefault();
            }
        }
    }
}

