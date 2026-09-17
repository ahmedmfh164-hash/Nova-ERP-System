using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Responses
{
    public sealed record CategoryResponseDTO
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

    }
}
