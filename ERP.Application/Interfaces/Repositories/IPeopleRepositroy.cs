using ERP.Core;
using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories
{
    public interface IPeopleRepository
    {
        public Task<List<Person>> GetAllPeopleAsync();
        public Task<int> AddPersonAsync(Person person);
        public Task<bool> EditPersonInfoAsync(Person updatedPerson);
        public Task<DeletedPersonResult?> DeletePersonAsync(int personId);
        public Task<Person> GetPersonByPersonIdAsync(int personId);
        public Task<bool> isPersonExistAsync(int personId);
        public Task<bool> isPersonExistAsync(string email);





    }
}
