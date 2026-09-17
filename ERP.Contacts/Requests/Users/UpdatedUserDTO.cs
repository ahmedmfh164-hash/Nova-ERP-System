using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Requests.Users
{
    public class UpdatedUserDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
