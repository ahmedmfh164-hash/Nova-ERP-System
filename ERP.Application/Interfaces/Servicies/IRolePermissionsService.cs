using ERP.Contacts.Requests.RolePermissions;
using ERP.Contacts.Requests.Roles;
using ERP.Core.Enums;
using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Servicies
{
    public interface IRolePermissionsService
    {
        public Task<int?> AddRolePermissionAsync(RolePermissions dto);
        public Task<bool?> DeletePermissionsOfRoleAsync(int roleId);
        public Task<bool?> DeleteOnePermissionsOfRoleAsync(RolePermissionsRequestDTO drp);
        public Task<List<RolePermissions?>> GetRolePermissionsByRoleIdAsync(int roleId);
        public Task<bool> IsRolePermissionsExistAsync(int roleId);
        public Task<bool> IsRolePermissionsExistAsync(RolePermissionsRequestDTO requestDTO);
        public Task<bool> HasPermissionAsync(string RoleName, PermissionModules module, PermissionAction action);

    }
}
