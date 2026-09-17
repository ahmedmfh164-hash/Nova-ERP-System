using ERP.Contacts.Requests.RolePermissions;
using ERP.Core.Enums;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Application.Interfaces.Repositories
{
    public interface IRolePermissionsRepository
    {
        public Task<int?> AddRolePermissionAsync(RolePermissions rolePermissions);
        public Task<bool> DeletePermissionsOfRoleAsync(int roleId);
        public Task<bool> DeletePermissionsOfRoleAsync(RolePermissionsRequestDTO drp);
        public Task<List<RolePermissions?>> GetRolePermissionsByRoleIdAsync(int roleId);
        public Task<bool> IsRolePermissionsExistAsync(int roleId);
        public Task<bool> IsRolePermissionsExistAsync(RolePermissionsRequestDTO requestDTO);
        public Task<bool> HasPermissionAsync(RolePermissions rp);


    }
}
