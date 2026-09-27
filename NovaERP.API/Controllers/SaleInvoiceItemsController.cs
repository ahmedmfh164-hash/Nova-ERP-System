using ERP.Application.Interfaces.Servicies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERP.API.Authorization;
using ERP.Core.Enums;
using ERP.Contacts.Requests.SaleInvoiceItems;

namespace ERP.API.Controllers
{
    [Route("api/SaleInvoiceItems")]
    [ApiController]
    [Authorize]
    public class SaleInvoiceItemsController : ControllerBase
    {
        private readonly ISaleInvoiceItemsService _SaleInvoiceItemsService;

        public SaleInvoiceItemsController(ISaleInvoiceItemsService saleInvoiceItemsService)
        {
            _SaleInvoiceItemsService = saleInvoiceItemsService;
        }

        [HttpGet("GetAll", Name = "GetAllSaleInvoiceItems")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSaleInvoiceItems()
        {
            var result = await _SaleInvoiceItemsService.GetAllSaleInvoiceItemsAsync();
            if (result.Count == 0)
                return NotFound("No Sale invoice items found.");

            return Ok(result);
        }

        [HttpGet("GetById/{SaleInvoiceItemId}", Name = "GetSaleInvoiceItemById")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSaleInvoiceItemById(int SaleInvoiceItemId)
        {
            if (SaleInvoiceItemId < 1)
                return BadRequest("Invalid Id");
            var result = await _SaleInvoiceItemsService.GetSaleInvoiceItemByIdAsync(SaleInvoiceItemId);

            if (result == null)
                return NotFound("Sale invoice item not found.");

            return Ok(result);
        }

        [HttpGet("GetByInvoiceId/{SaleInvoiceId}", Name = "GetSaleInvoiceItemsByInvoiceId")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSaleInvoiceItemsByInvoiceId(int SaleInvoiceId)
        {
            if (SaleInvoiceId < 1)
                return BadRequest("Invalid Id");
            var result = await _SaleInvoiceItemsService.GetItemsBySaleInvoiceIdAsync(SaleInvoiceId);
            if (result.Count==0)
                return NotFound("Sale invoice items not found.");

            return Ok(result);
        }

        [HttpPost("Add", Name = "AddSaleInvoiceItem")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSaleInvoiceItem(CreateSaleInvoiceItemDTO DTO)
        {
            var result = await _SaleInvoiceItemsService.AddSaleInvoiceItemAsync(DTO);

            if (result <= 0)
                return BadRequest("Failed to add sales invoice item.Make sure the invoice is in Draft status.");

            return CreatedAtRoute("GetSaleInvoiceItemById",
                new { SaleInvoiceItemId = result },$"Done added sale invoice item succesfully with id: {result}");
        }

        [HttpPut("Update", Name = "UpdateSaleInvoiceItem")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSaleInvoiceItem(UpdateSaleInvoiceItemDTO DTO)
        {
            var result = await _SaleInvoiceItemsService.EditSaleInvoiceItemAsync(DTO);

            if (!result)
                return BadRequest("Sale invoice item cannot be updated." +
                    "Make sure it exists,is in Draft status.");

            return Ok("Sale invoice updated successfully.");
        }

        [HttpDelete("Delete/{SaleInvoiceItemId}", Name = "DeleteSaleInvoiceItem")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSaleInvoiceItem(int SaleInvoiceItemId)
        {
            if (SaleInvoiceItemId < 1)
                return BadRequest("Invalid Id");
            var result = await _SaleInvoiceItemsService.DeleteSaleInvoiceItemAsync(SaleInvoiceItemId);

            if (!result)
                return BadRequest("Sale invoice item cannot be deleted." +
                    "Make sure it exists,is in Draft status.");

            return Ok("Sale invoice deleted successfully.");
        }
    }
}
