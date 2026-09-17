using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.RolePermissions;
using ERP.Contacts.Requests.Roles;
using ERP.Core.Enums;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Application.Servicies
{
    public class RolePermissionsService:IRolePermissionsService
    {
        private readonly IRolePermissionsRepository _repo;
        public RolePermissionsService(IRolePermissionsRepository repo)
        {
            _repo= repo;
        }


        public async Task<int?> AddRolePermissionAsync(RolePermissions dto)
        {
            int? roleId = await _repo.AddRolePermissionAsync(dto);

            return roleId;
        }

        public async Task<bool?> DeletePermissionsOfRoleAsync(int roleId)
        {
            bool? isDeleted = await _repo.DeletePermissionsOfRoleAsync(roleId);

            return isDeleted;
        }

        public async Task<bool?> DeleteOnePermissionsOfRoleAsync(RolePermissionsRequestDTO drp)
        {
            bool? isDeleted = await _repo.DeletePermissionsOfRoleAsync(drp);

            return isDeleted;
        }

        public async Task<List<RolePermissions?>> GetRolePermissionsByRoleIdAsync(int roleId)
        {
            var rolePermissions = await _repo.GetRolePermissionsByRoleIdAsync(roleId);

            return rolePermissions.ToList();
        }

        public async Task<bool> IsRolePermissionsExistAsync(int roleId)
        {
            return await _repo.IsRolePermissionsExistAsync(roleId);

        }

        public async Task<bool> IsRolePermissionsExistAsync(RolePermissionsRequestDTO requestDTO)
        {
            return await _repo.IsRolePermissionsExistAsync(requestDTO);

        }

        public async Task<bool> HasPermissionAsync(string RoleName, PermissionModules module, PermissionAction action)
        {
            RolePermissions rp=new RolePermissions (RoleName ,module.ToString(),action.ToString());

            return await _repo.HasPermissionAsync(rp);
        }


    }
}
