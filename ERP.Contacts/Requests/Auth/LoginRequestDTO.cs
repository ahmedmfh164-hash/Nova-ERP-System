using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Requests.Auth
{
    public class LoginRequestDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}
