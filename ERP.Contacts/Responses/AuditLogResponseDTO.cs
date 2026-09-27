using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Responses
{
    public sealed record AuditLogResponseDTO
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public DateTime LogDate { get; set; }
    }
}

