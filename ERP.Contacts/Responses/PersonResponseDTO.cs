

using ERP.Core.Enums;

namespace ERP.Contacts.Responses
{
    public sealed record PersonResponseDTO
    {
        public int PersonId { get; set; }
        public string? PersonName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
        public Guid? ImageGuid { get; set; }
    }
}
