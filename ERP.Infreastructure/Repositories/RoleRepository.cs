using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;
using ERP.Application.Interfaces.Repositories;

namespace ERP.Infreastructure.Repositories
{
    public class RoleRepository :IRoleRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public RoleRepository(IDBConnectionFactory dbConnectionFactory,
        IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }


        private Role MapToRole(SqlDataReader reader)
        {
            return new Role
            (
                roleId: reader.GetInt32(reader.GetOrdinal("RoleId")),
                roleName: reader.GetString(reader.GetOrdinal("RoleName"))
            );

        }


        public async Task<List<Role>> GetAllRolesAsync()
        {
            List<Role> list = new List<Role>();

            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_AllRoles", con);

            list= await _StoredProcedture.ExecuteListAsync(cmd, con, MapToRole);

            return list;
        }

        public async Task<int?> AddRoleAsync(string roleName)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_AddRole", con);

            SqlCommandExtentions.AddParameters(cmd, "@RoleName", roleName);

            int? roleId = await _StoredProcedture.ExecuteScalarAsync(cmd, con);

            return roleId;
        }

        public async Task<bool> UpdateRoleAsync(Role updatedRole)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_UpdateRole", con);

            SqlCommandExtentions.AddParameters(cmd, updatedRole);

            return (await _StoredProcedture.ExecuteNonQueryAsync(cmd, con)>0);

        }

        public async Task<bool> DeleteRoleAsync(int roleId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_DeleteRole", con);

            SqlCommandExtentions.AddParameters(cmd, "@RoleId", roleId);

            return (await _StoredProcedture.ExecuteNonQueryAsync(cmd, con)>0);

        }


        public async Task<Role?> GetRoleByRoleIdAsync(int roleId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_GetRoleByRoleId", con);

            SqlCommandExtentions.AddParameters(cmd, "@RoleId", roleId);

            Role role = await _StoredProcedture.ExecuteSingleAsync(cmd, con, MapToRole);

            return role;
        }


        public async Task<bool> isRoleExistAsync(int roleId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_isRoleExistbyId", con);

            SqlCommandExtentions.AddParameters(cmd, "@RoleId", roleId);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }

        public async Task<bool> isRoleExistAsync(string roleName)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_isRoleExistbyName", con);

            SqlCommandExtentions.AddParameters(cmd, "@RoleName", roleName);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }


    }
}
