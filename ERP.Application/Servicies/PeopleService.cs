using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.People;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using ERP.Application.Interfaces.Helpers;
namespace ERP.Application.Servicies
{
    public class PeopleService :IPeopleService
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IDirectoryPathService _directoryPathService;
        public PeopleService(IPeopleRepository repo, IFileStorageService fileStorageService, IDirectoryPathService directoryPathService)
        {
            _peopleRepository = repo;
            _fileStorageService = fileStorageService;
            _directoryPathService = directoryPathService;
        }

        public async Task<List<PersonResponseDTO>> GetAllPeopleAsync()
        {
            List<Person> people = await _peopleRepository.GetAllPeopleAsync();

           return people.Select(person => person.ToResponseDTO()).ToList();

        }

        public async Task<PersonResponseDTO> AddPersonAsync(CreatePersonDTO dto)
        {
            Guid? imageGuid = dto.Image != null
                ? Guid.NewGuid()
                : null;

            Person person = dto.ToEntity(imageGuid);

            person.PersonId =await _peopleRepository.AddPersonAsync(person);

            if (dto.Image != null && imageGuid.HasValue)
            {
                await _fileStorageService.SaveFileAsync( dto.Image,imageGuid, _directoryPathService.Directory);
            }

            return person.ToResponseDTO();
        }
        

        public async Task<bool> UpdatePersonInfoAsync(UpdatedPersonDTO updatedPerson)
        {
            bool isUpdated = false;

            if(updatedPerson.ImageFile != null)
            {
                var existingPerson = await _peopleRepository.GetPersonByPersonIdAsync(updatedPerson.PersonId);
                if (existingPerson == null)
                {
                    return false;
                }

                if (existingPerson.ImageGuid!=null)
                {
                    await _fileStorageService.DeleteFileAsync(existingPerson.ImageGuid, _directoryPathService.Directory);
                }
                
                Guid newImageGuid = Guid.NewGuid();

               isUpdated= await _peopleRepository.EditPersonInfoAsync(updatedPerson.ToEntity(newImageGuid));

              await _fileStorageService.SaveFileAsync(updatedPerson.ImageFile, newImageGuid, _directoryPathService.Directory);
                
            }

            return isUpdated;
        }

        public async Task<bool> DeletePersonAsync(int PersonId)
        {
            var deletedPerson = await _peopleRepository.DeletePersonAsync(PersonId);

            if(deletedPerson==null)
                { return false; }

            if (deletedPerson.ImageGuid!=null&&deletedPerson.PersonId>0)
            {
                await _fileStorageService.DeleteFileAsync(deletedPerson.ImageGuid, _directoryPathService.Directory);
            }
           
            return true;
        }

        public async Task<PersonResponseDTO?> GetPersonByPersonIdAsync(int personId)
        {
            var person = await _peopleRepository.GetPersonByPersonIdAsync(personId);

            return person == null ? null
                : person.ToResponseDTO();
        }

        public async Task<bool> IsPersonExistAsync(int PersonId)
        {
            return await _peopleRepository.isPersonExistAsync(PersonId);

        }

        public async Task<bool> IsPersonExistAsync(string email)
        {
            return await _peopleRepository.isPersonExistAsync(email);

        }


    }
}
