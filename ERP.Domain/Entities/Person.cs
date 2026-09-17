using ERP.Core.Enums;

namespace ERP.Domain.Entities
{
    public class Person
    {
        private string _PersonName = null!;

        public int PersonId { get; set; }

        public string PersonName
        {
            get => _PersonName;
            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(PersonName));
                _PersonName = value.Trim();
            }
        }

        public string Phone { get; set; }
        public string? Email { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public Guid? ImageGuid { get; set; }

        public Person(int PersonId, string PersonName, string Phone, string? Email, string Address, Gender Gender, Guid? ImageGuid = null)
        {
            this.PersonId=PersonId;
            this.PersonName=PersonName;
            this.Phone=Phone;
            this.Email=Email;
            this.Address=Address;
            this.Gender=Gender;
            this.ImageGuid=ImageGuid;
        }


    }
}
