using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.Supplier;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [Route("api/Suppliers")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplier;

        public SuppliersController(ISupplierService supplier)
        {
            _supplier = supplier;
        }


        [HttpGet("AllSuppliers", Name = "GetAllSuppliersAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SupplierResponseDTO>>> GetAllSuppliersAsync()
        {
            List<SupplierResponseDTO> supplierList = await _supplier.GetAllSupplierAsync();

            if (supplierList.Count == 0)
                return NotFound("No suppliers found.");

            return Ok(supplierList);
        }


        [HttpGet("GetSupplier/{SupplierId}", Name = "GetSupplierBySupplierIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetSupplierBySupplierIdAsync(int SupplierId)
        {
            if (SupplierId < 1)
                return BadRequest("Invalid Data");

            var supplier = await _supplier.GetSupplierBySupplierIdAsync(SupplierId);

            if (supplier == null)
                return NotFound("Supplier not found");

            return Ok(supplier);
        }


        [HttpPost("AddSupplier", Name = "AddSupplierAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddSupplierAsync([FromForm] CreatedSupplierDTO dto)
        {
            if (dto==null||dto.PersonId<1)
            {
                return BadRequest("Invalid Data");
            }

            var result = await _supplier.AddSupplierAsync(dto);

            if (result==0)
                return NotFound("This person is not found.");

            return Ok($"Done added supplier successfully with Id : {result}");
        }


        [HttpDelete("Delete/{SupplierId}", Name = "DeleteSupplierAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteSupplierAsync(int SupplierId)
        {
            if (SupplierId<1)
                return BadRequest("Invalid Id");

            if (await _supplier.DeleteSupplierAsync(SupplierId))
                return Ok("Done delete supplier successfully.");
            else
                return NotFound("This supplier is not found!");

        }


        [HttpGet("exist/{SupplierId}", Name = "IsSupplierExistByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsSupplierExistByIdAsync(int SupplierId)
        {
            bool isFound = await _supplier.IsSupplierExistAsync(SupplierId);

            if (!isFound)
                return NotFound("Not Found");

            return Ok(isFound);
        }



    }
}
