using ERP.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace ERP.Contacts.Requests.People
{
    public sealed record UpdatedPersonDTO
    {
        public int PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
