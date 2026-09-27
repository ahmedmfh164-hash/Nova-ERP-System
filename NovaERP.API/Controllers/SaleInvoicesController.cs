using ERP.API.Authorization;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.SaleInvoices;
using ERP.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERP.API.Controllers
{
    [Route("api/SaleInvoices")]
    [ApiController]
    [Authorize]
    public class SaleInvoicesController : ControllerBase
    {
        private readonly ISaleInvoicesService _SaleInvoicesService;

        public SaleInvoicesController(
            ISaleInvoicesService saleInvoicesService)
        {
            _SaleInvoicesService = saleInvoicesService;
        }


        [HttpGet("GetAll", Name = "GetAllSaleInvoices")]
        [HasPermission( PermissionModules.Sales, PermissionAction.Read)]
 
        public async Task<IActionResult> GetAllSaleInvoices()
        {
            var result = await _SaleInvoicesService.GetAllSaleInvoicesAsync();
              if (result.Count == 0)
                return NotFound("No Sale invoices found.");
            return Ok(result);
        }


        [HttpGet("GetById/{SaleInvoiceId}", Name = "GetSaleInvoiceById")]
        [HasPermission(PermissionModules.Sales,PermissionAction.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSaleInvoiceById(int SaleInvoiceId)
        {
            if (SaleInvoiceId < 1)
                return BadRequest("Invalid Id");
            var result = await _SaleInvoicesService.GetSaleInvoiceByIdAsync(SaleInvoiceId);

            if (result == null)
                return NotFound("No Sale invoice found.");

            return Ok(result);
        }


        [HttpPost("Add", Name = "AddSaleInvoice")]
        [HasPermission( PermissionModules.Sales,PermissionAction.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSaleInvoice(CreateSaleInvoiceDTO DTO)
        {

            var UserId = int.Parse( User.FindFirst( ClaimTypes.NameIdentifier)!.Value);

            var result =await _SaleInvoicesService.AddSaleInvoiceAsync(DTO, UserId);

            if (result <= 0)
                return BadRequest("Failed to add sales invoice.");

            return CreatedAtRoute(
                "GetSaleInvoiceById", new { SaleInvoiceId = result },$"Done added sale invoice succesfully with id: {result}");
        }


        [HttpDelete("Delete/{SaleInvoiceId}",Name = "DeleteSaleInvoice")]
        [HasPermission(PermissionModules.Sales,PermissionAction.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSaleInvoice(int SaleInvoiceId)
        {
            if (SaleInvoiceId < 1)
                return BadRequest("Invalid Id");
            var result =await _SaleInvoicesService.DeleteSaleInvoiceAsync(SaleInvoiceId);

            if (!result)
                return BadRequest("Sale invoice cannot be deleted." +
                    "Make sure it exists,is in Drift status.");

            return Ok("Sale invoice deleted successfully.");
        }


        [HttpPost( "Confirm/{SaleInvoiceId}", Name = "ConfirmSaleInvoice")]
        [HasPermission( PermissionModules.Sales, PermissionAction.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmSaleInvoice(int SaleInvoiceId)
        {
            if (SaleInvoiceId < 1)
                return BadRequest("Invalid Id");
            var result = await _SaleInvoicesService.ConfirmSaleInvoiceAsync(SaleInvoiceId);

            if (!result)
                return BadRequest("Sale invoice cannot be confirmed." +
                    "Make sure it exists,is in Drift status,and contains items.");

            return Ok("Sale invoice confirmed successfully.");
        }


        [HttpPost("Cancel/{SaleInvoiceId}",Name = "CancelSaleInvoice")]
        [HasPermission(PermissionModules.Sales,PermissionAction.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelSaleInvoice(int SaleInvoiceId)
        {
            if (SaleInvoiceId < 1)
                return BadRequest("Invalid Id");
            var result =
                await _SaleInvoicesService.CancelSaleInvoiceAsync(SaleInvoiceId);

            if (!result)
                return BadRequest("Sale invoice cannot be cancelled." +
                    "Make sure it exists,is in Posted status.");

            return Ok("Sale invoice cancelled successfully.");
        }
    }
}