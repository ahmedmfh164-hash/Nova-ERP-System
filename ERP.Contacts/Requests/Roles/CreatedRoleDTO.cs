using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Requests.Roles
{
    public sealed record CreatedRoleDTO
    {
        public string RoleName { get; set; }
    }
}
