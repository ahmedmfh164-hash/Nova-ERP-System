using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Requests.Auth
{
    public sealed record LogoutRequestDTO
    {
        public string RefreshToken { get; set; }
        public string Email { get; set; }
    }
}
