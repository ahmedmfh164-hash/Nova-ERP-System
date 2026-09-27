using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.SaleReturns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERP.API.Authorization;
using ERP.Core.Enums;
using ERP.Contacts.Responses;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleReturnsController : ControllerBase
    {
        private readonly ISaleReturnsService _SaleReturnsService;

        public SaleReturnsController(ISaleReturnsService saleReturnsService)
        {
            _SaleReturnsService = saleReturnsService;
        }

        [HttpGet("GetAll", Name = "GetAllSaleReturns")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SaleReturnResponseDTO>>> GetAllSaleReturns()
        {
            var result = await _SaleReturnsService.GetAllSaleReturnsAsync();
            if (result.Count==0)
                return NotFound("Sale returns not found.");

            return Ok(result);
        }

        [HttpGet("GetById/{ReturnId}", Name = "GetSaleReturnById")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetSaleReturnById(int ReturnId)
        {
            if (ReturnId<1)
                return BadRequest("Invalid Data");

            var result = await _SaleReturnsService.GetSaleReturnByIdAsync(ReturnId);

            if (result == null)
                return NotFound("Sale return not found.");

            return Ok(result);
        }

        [HttpGet("GetBySaleInvoiceId/{SaleInvoiceId}", Name = "GetSaleReturnsBySaleInvoiceId")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SaleReturnItemResponseDTO>>> GetSaleReturnsBySaleInvoiceId(int SaleInvoiceId)
        {
            if (SaleInvoiceId<1)
                return BadRequest("Invalid Data");

            var result = await _SaleReturnsService.GetSaleReturnsBySaleInvoiceIdAsync(SaleInvoiceId);

            if (result.Count==0)
                return NotFound("Sale returns not found.");

            return Ok(result);
        }

        [HttpPost("Add", Name = "AddSaleReturn")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddSaleReturn(CreateSaleReturnDTO DTO)
        {
            if (DTO.SaleInvoiceId<1||DTO.ReasonReturn==string.Empty)
                return BadRequest("Invalid Data");

            var result = await _SaleReturnsService.AddSaleReturnAsync(DTO);

            if (result <= 0)
                return BadRequest("Failed to add sale return.");

            return CreatedAtRoute("GetSaleReturnById", new { ReturnId = result }, $"Done added sale return succesfully with id: {result}");
        }

        [HttpPut("Update", Name = "UpdateSaleReturn")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSaleReturn([FromForm]UpdateSaleReturnDTO DTO)
        {
            if (DTO.ReturnId<1||DTO.ReasonReturn==string.Empty)
                return BadRequest("Invalid Data");

            var result = await _SaleReturnsService.EditSaleReturnAsync(DTO);

            if (!result)
                return NotFound("Sale return not found.");

            return Ok("Sale return updated successfully.");
        }

        [HttpDelete("Delete/{ReturnId}", Name = "DeleteSaleReturn")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSaleReturn(int ReturnId)
        {
            if (ReturnId<1)
                return BadRequest("Invalid Data");

            var result = await _SaleReturnsService.DeleteSaleReturnAsync(ReturnId);

            if (!result)
                return NotFound("Sale return not found.");

            return Ok("Sale return deleted successfully.");
        }
    }
}
