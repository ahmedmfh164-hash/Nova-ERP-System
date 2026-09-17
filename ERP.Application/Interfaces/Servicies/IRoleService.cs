using ERP.Contacts.Requests.Roles;
using ERP.Domain.Entities;
using System;

namespace ERP.Application.Interfaces.Servicies
{
    public interface IRoleService
    {
        public Task<List<Role>> GetAllRolesAsync();
        public Task<int?> AddRoleAsync(CreatedRoleDTO dto);
        public Task<bool> UpdateRoleAsync(Role updatedrole);
        public Task<bool?> DeleteRoleAsync(int roleId);
        public Task<Role?> GetRoleByRoleIdAsync(int roleId);
        public Task<bool> IsRoleExistAsync(int roleId);
        public Task<bool> IsRoleExistAsync(string roleName);



    }
}
