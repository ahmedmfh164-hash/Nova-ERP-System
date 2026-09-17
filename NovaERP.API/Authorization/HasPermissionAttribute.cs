using ERP.Core.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ERP.API.Authorization
{
    public class HasPermissionAttribute :AuthorizeAttribute
    {
        public HasPermissionAttribute(PermissionModules module,PermissionAction action)  => Policy=$"{module}.{action}";
        
    }
}
