using ERP.Contacts.Requests.People;
using ERP.Domain.Entities;
using ERP.Contacts.Responses;
using ERP.Contacts.Requests.Supplier;

namespace ERP.Contacts.Mappings
{
    public static class PersonMapping
    {
        public static Person ToEntity(this CreatePersonDTO dto, Guid? imageGuid = null)
        {
            return new Person(
                0,
                dto.PersonName,
                dto.Phone,
                dto.Email,
                dto.Address,
                dto.Gender,
                imageGuid);
        }


        public static Person ToEntity(this UpdatedPersonDTO dto, Guid? imageGuid = null)
        {
            return new Person(
                dto.PersonId,
                dto.PersonName,
                dto.Phone,
                dto.Email,
                dto.Address,
                dto.Gender,
                imageGuid);
        }

        public static PersonResponseDTO ToResponseDTO(this Person person)
        {
            return new PersonResponseDTO
            {
                PersonId = person.PersonId,
                PersonName = person.PersonName,
                Phone = person.Phone,
                Email = person.Email,
                Address = person.Address,
                Gender = person.Gender,
                ImageGuid = person.ImageGuid

            };
        }



    }
}