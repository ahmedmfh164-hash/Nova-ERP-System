using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Requests.RolePermissions
{
    public sealed record RolePermissionsRequestDTO
    {
        public int? RoleId { get; set; }
        public string? Module { get; set; }
        public string? Action { get; set; }
    }
}
