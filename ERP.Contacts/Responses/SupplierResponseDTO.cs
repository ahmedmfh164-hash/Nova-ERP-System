using ERP.Core.Enums;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Responses
{
    public sealed record SupplierResponseDTO
    {
        public int SupplierId { get; set; }
        public int PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
        public Guid? ImageGuid { get; set; }


    }
}
