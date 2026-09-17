using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Contacts.Requests.RolePermissions ;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Infreastructure.Repositories
{
    public class RolePermissionsRepository:IRolePermissionsRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public RolePermissionsRepository(IDBConnectionFactory dbConnectionFactory,
        IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }


        private RolePermissions MapToRolePermissions(SqlDataReader reader)
        {
            return new RolePermissions
            (
                roleName: reader.GetString(reader.GetOrdinal("RoleName")),
                module: reader.GetString(reader.GetOrdinal("Module")),
                action: reader.GetString(reader.GetOrdinal("Action"))
            );

        }

        public async Task<int?> AddRolePermissionAsync(RolePermissions rolePermissions)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_AddRolePermissions", con);

            SqlCommandExtentions.AddParameters(cmd,rolePermissions);

            int? roleId = await _StoredProcedture.ExecuteScalarAsync(cmd, con);

            return roleId;
        }

        public async Task<bool> DeletePermissionsOfRoleAsync(int roleId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_DeleteRolePermissions", con);

            SqlCommandExtentions.AddParameters(cmd, "@RoleId", roleId);

            return (await _StoredProcedture.ExecuteNonQueryAsync(cmd, con)>0);

        }

        public async Task<bool> DeletePermissionsOfRoleAsync(RolePermissionsRequestDTO drp)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_DeleteOneRolePermissions", con);

            SqlCommandExtentions.AddParameters(cmd,drp);

            return (await _StoredProcedture.ExecuteNonQueryAsync(cmd, con)>0);

        }

        public async Task<List<RolePermissions?>> GetRolePermissionsByRoleIdAsync(int roleId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_GetRolePermissionsByRoleId", con);

            SqlCommandExtentions.AddParameters(cmd, "@RoleId", roleId);

            List<RolePermissions?> rolePermissions = await _StoredProcedture.ExecuteListAsync(cmd, con, MapToRolePermissions);

            return rolePermissions.ToList();
        }


        public async Task<bool> IsRolePermissionsExistAsync(int roleId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_isRolePermissionsExistbyId", con);

            SqlCommandExtentions.AddParameters(cmd, "@RoleId", roleId);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }

        public async Task<bool> IsRolePermissionsExistAsync(RolePermissionsRequestDTO requestDTO)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_isOneRolePermissionsExists", con);

            SqlCommandExtentions.AddParameters(cmd,requestDTO);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }


        public async Task<bool> HasPermissionAsync(RolePermissions rp)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("dbo.usp_HasPermission", con);

            SqlCommandExtentions.AddParameters(cmd,rp);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }



    }
}
