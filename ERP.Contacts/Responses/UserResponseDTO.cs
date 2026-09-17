using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Responses
{
    public class UserResponseDTO
    {
        public int UserId { get; set; }
        public int PersonId { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}
