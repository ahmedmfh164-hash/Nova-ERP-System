using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetAllRolesAsync();
        public Task<int?> AddRoleAsync(string roleName);
        public Task<bool> UpdateRoleAsync(Role updatedRole);
        public Task<bool> DeleteRoleAsync(int roleId);
        public Task<Role?> GetRoleByRoleIdAsync(int roleId);
        public Task<bool> isRoleExistAsync(int roleId);
        public Task<bool> isRoleExistAsync(string roleName);



    }
}
