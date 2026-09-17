using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Domain.Entities
{
    public class RolePermissions
    {
       public string? RoleName { get; set; }
       public string? Module { get; set; }
       public string? Action { get; set; }

        public RolePermissions(string? roleName,string? module,string? action)
        {
            RoleName = roleName;
            Module = module;
            Action= action;
        }

    }
}
