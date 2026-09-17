using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.Supplier;
using ERP.Contacts.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ERP.Contacts.Requests.Customer;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customer;

        public CustomerController(ICustomerService customer)
        {
            _customer = customer;
        }


        [HttpGet("AllCustomers", Name = "GetAllCustomersAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CustomerResponseDTO>>> GetAllCustomersAsync()
        {
            List<CustomerResponseDTO> customerList = await _customer.GetAllCustomersAsync();

            if (customerList.Count == 0)
                return NotFound("No customers found.");

            return Ok(customerList);
        }


        [HttpGet("GetCustomer/{CustomerId}", Name = "GetCustomerByCustomerIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCustomerByCustomerIdAsync(int CustomerId)
        {
            if (CustomerId < 1)
                return BadRequest("Invalid Data");

            var customer = await _customer.GetCustomerByCustomerIdAsync(CustomerId);

            if (customer == null)
                return NotFound("Customer not found");

            return Ok(customer);
        }


        [HttpPost("AddCustomer", Name = "AddCustomerAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddCustomerAsync([FromForm] CreatedCustomerDTO dto)
        {
            if (dto==null||dto.PersonId<1)
            {
                return BadRequest("Invalid Data");
            }

            var result = await _customer.AddCustomerAsync(dto);

            if (result==0)
                return NotFound("This customer is not found.");

            return Ok($"Done added customer successfully with Id : {result}");
        }


        [HttpDelete("Delete/{CustomerId}", Name = "DeleteCustomerAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCustomerAsync(int CustomerId)
        {
            if (CustomerId<1)
                return BadRequest("Invalid Id");

            if (await _customer.DeleteCustomerAsync(CustomerId))
                return Ok("Done delete custmer successfully.");
            else
                return NotFound("This customer is not found!");

        }


        [HttpGet("exist/{CustomerId}", Name = "IsCustomerExistByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsCustomerExistByIdAsync(int CustomerId)
        {
            bool isFound = await _customer.IsCustomerExistAsync(CustomerId);

            if (!isFound)
                return NotFound("Not Found");

            return Ok(isFound);
        }




    }
}
