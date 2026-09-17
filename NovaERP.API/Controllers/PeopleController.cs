using ERP.Application.Interfaces.Servicies;
using Microsoft.AspNetCore.Mvc;
using ERP.Core;
using ERP.Contacts.Requests.People;
using ERP.Contacts.Responses;

namespace ERP.API.Controllers
{
    [Route("api/People")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _people;

        public PeopleController(IPeopleService people)
        {
            _people = people;
        }



        [HttpGet("AllPeople",Name ="GetAllPeopleAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PersonResponseDTO>>> GetAllPeopleAsync()
        {
            List<PersonResponseDTO> peopleList = await _people.GetAllPeopleAsync();

            if (peopleList.Count == 0)
                return NotFound("No people found.");

            return Ok(peopleList);
        }


        [HttpGet("GetPerson/{PersonId}", Name = "GetPersonByPersonIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetPersonByPersonIdAsync(int PersonId)
        {
            if (PersonId < 1)
                return BadRequest("Invalid Data");

            var person = await _people.GetPersonByPersonIdAsync(PersonId);

            if (person == null)
                return NotFound("Person not found");

            return Ok(person);
        }

        [HttpPost("AddPerson", Name = "AddPersonAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddPersonAsync([FromForm] CreatePersonDTO dto)
        {
            if (dto == null||string.IsNullOrEmpty(dto.PersonName)||string.IsNullOrEmpty(dto.Address)||string.IsNullOrEmpty(dto.Phone))
            {
                return BadRequest("Invalid Data");
            }
            var result = await _people.AddPersonAsync(dto);

            return CreatedAtRoute("GetPersonByPersonIdAsync", new { PersonId = result.PersonId }, result);
        }
      

        [HttpPut("UpdateInfo", Name = "UpdatePersonInfoAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdatePersonInfoAsync([FromForm] UpdatedPersonDTO updatedPerson)
        {

            if (updatedPerson == null||string.IsNullOrEmpty(updatedPerson.PersonName)||string.IsNullOrEmpty(updatedPerson.Address)
                ||string.IsNullOrEmpty(updatedPerson.Phone))
            {
                return BadRequest("Invalid Data");
            }

            PersonResponseDTO person=await _people.GetPersonByPersonIdAsync(updatedPerson.PersonId);

            if(person == null)
                return NotFound("This person is not found!");

            if (await _people.IsPersonExistAsync(updatedPerson.Email)&&person.Email!=updatedPerson.Email)
                return BadRequest("This email is found!choose anuthor email.");

            return await _people.UpdatePersonInfoAsync( updatedPerson) ? Ok("Person updated successfully") : NotFound("Person not found");
        }


        [HttpDelete("Delete/{PersonId}", Name = "DeletePersonAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeletePersonAsync(int PersonId)
        {
            if (PersonId<1)
                return BadRequest("Invalid Id");

                if (await _people.DeletePersonAsync(PersonId))
                    return Ok("Done delete person successfully.");
                else
                    return NotFound("This person is not found!");

        }


        [HttpGet("exist/{PersonId}", Name = "IsPersonExistByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsPersonExistByIdAsync(int PersonId)
        {
            bool isFound = await _people.IsPersonExistAsync(PersonId);

            if (!isFound)
                return NotFound("Not Found");

            return Ok(isFound);
        }

        [HttpGet("exists/{email}", Name = "IsPersonExistByEmailAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsPersonExistByEmailAsync(string email)
        {
            bool isFound = await _people.IsPersonExistAsync(email);

            if (!isFound)
                return NotFound("Not Found");

            return Ok(isFound);
        }

   

    }
}
