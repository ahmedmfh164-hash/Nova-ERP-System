using ERP.Application.Interfaces.Repositories;
using ERP.Domain.Entities;
using ERP.Contacts.Requests.Roles;
using ERP.Application.Interfaces.Servicies;

namespace ERP.Application.Servicies
{
    public class RoleService:IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository repo)
        {
            _roleRepository = repo;
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            List<Role> roles = await _roleRepository.GetAllRolesAsync();

            return roles.ToList();

        }

        public async Task<int?> AddRoleAsync(CreatedRoleDTO dto)
        {
            int? roleId =await _roleRepository.AddRoleAsync(dto.RoleName);

            return roleId;
        }


        public async Task<bool> UpdateRoleAsync(Role updatedrole)
        {
               bool isUpdated= await _roleRepository.UpdateRoleAsync(updatedrole);

            return isUpdated;
        }

        public async Task<bool?> DeleteRoleAsync(int roleId)
        {
            bool? isDeleted= await _roleRepository.DeleteRoleAsync(roleId);

            return isDeleted;
        }

        public async Task<Role?> GetRoleByRoleIdAsync(int roleId)
        {
            var role = await _roleRepository.GetRoleByRoleIdAsync(roleId);

            return role == null ? null:role;
        }

        public async Task<bool> IsRoleExistAsync(int roleId)
        {
            return await _roleRepository.isRoleExistAsync(roleId);

        }

        public async Task<bool> IsRoleExistAsync(string roleName)
        {
            return await _roleRepository.isRoleExistAsync(roleName);

        }



    }
}
