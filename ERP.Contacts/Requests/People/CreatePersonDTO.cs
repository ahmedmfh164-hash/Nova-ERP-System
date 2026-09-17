using ERP.Contacts.Mappings;
using Microsoft.AspNetCore.Http;
using ERP.Domain.Entities;
using ERP.Core.Enums;

namespace ERP.Contacts.Requests.People
{
    public sealed record CreatePersonDTO
    {
        public string PersonName { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
        public IFormFile? Image { get; set; }
         public Person ToEntity(Guid? imageGuid = null)
        {
            return PersonMapping.ToEntity(this, imageGuid);
        }
    }
}