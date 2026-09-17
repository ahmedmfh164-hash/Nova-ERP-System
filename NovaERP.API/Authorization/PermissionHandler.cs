using ERP.Application.Authorization;
using ERP.Core.Enums;
using ERP.Domain;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ERP.Application.Interfaces.Servicies;

namespace ERP.API.Authorization
{
    public sealed class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRolePermissionsService _rolePermissionsService;

        public PermissionHandler(IHttpContextAccessor httpContextAccessor,IRolePermissionsService rolePermissionsService)
        {
            _httpContextAccessor = httpContextAccessor;
            _rolePermissionsService = rolePermissionsService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,PermissionRequirement requirement)
        {
          
            if (context.User.IsInRole(Roles.Admin.ToString()))
            {
                    context.Succeed(requirement);
            }

            if (await _rolePermissionsService.HasPermissionAsync(context.User.FindFirst(ClaimTypes.Role).ToString()
                , requirement.Permission.Module, requirement.Permission.Action))
                context.Succeed(requirement);
        }

     
   
    }

    
}
