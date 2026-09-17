using ERP.Domain;
using Microsoft.AspNetCore.Authorization;


namespace ERP.Application.Authorization
{
    public sealed class PermissionRequirement : IAuthorizationRequirement
    {
        public Permission Permission { get; }

        public PermissionRequirement(Permission permission) => Permission = permission;
        
    }
}
