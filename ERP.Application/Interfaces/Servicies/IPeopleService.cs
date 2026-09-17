using ERP.Contacts.Requests.People;
using ERP.Contacts.Responses;

namespace ERP.Application.Interfaces.Servicies
{
    public interface IPeopleService
    {
        public Task<List<PersonResponseDTO>> GetAllPeopleAsync();
        public Task<PersonResponseDTO> AddPersonAsync(CreatePersonDTO person);
        public Task<bool> UpdatePersonInfoAsync(UpdatedPersonDTO updatedPerson);
        public Task<bool> DeletePersonAsync(int PersonId);
        public Task<PersonResponseDTO> GetPersonByPersonIdAsync(int PersonId);
        public Task<bool> IsPersonExistAsync(int PersonId);
        public Task<bool> IsPersonExistAsync(string email);





    }
}
