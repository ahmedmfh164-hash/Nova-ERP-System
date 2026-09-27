using ERP.API.Authorization;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.PurchaseInvoiceItems;
using ERP.Contacts.Responses;
using ERP.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace ERP.API.Controllers
{
    [Authorize]
    [Route("api/PurchaseInvoiceItems")]
    [ApiController]
    public class PurchaseInvoiceItemsController : ControllerBase
    {
        private readonly IPurchaseInvoiceItemsService _service;

        public PurchaseInvoiceItemsController(IPurchaseInvoiceItemsService service)
        {
            _service = service;
        }

        [HasPermission(PermissionModules.PurchaseInvoiceItems, PermissionAction.Read)]
        [HttpGet("AllItems", Name = "GetAllPurchaseInvoiceItemsAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PurchaseInvoiceItemResponseDTO>>> GetAllPurchaseInvoiceItemsAsync()
        {
            var list = await _service.GetAllPurchaseInvoiceItemsAsync();
            if (list.Count == 0)
                return NotFound("No purchase invoice items found.");
            return Ok(list);
        }

        [HasPermission(PermissionModules.PurchaseInvoiceItems, PermissionAction.Read)]
        [HttpGet("GetItem/{purchaseInvoiceItemId}", Name = "GetPurchaseInvoiceItemByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetPurchaseInvoiceItemByIdAsync(int purchaseInvoiceItemId)
        {
            if (purchaseInvoiceItemId < 1)
                return BadRequest("Invalid Id.");

            var item = await _service.GetPurchaseInvoiceItemByIdAsync(purchaseInvoiceItemId);
            if (item == null)
                return NotFound("Purchase invoice item not found.");

            return Ok(item);
        }

        [HasPermission(PermissionModules.PurchaseInvoiceItems, PermissionAction.Read)]
        [HttpGet("GetItems/{purchaseInvoiceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetItemsByPurchaseInvoiceIdAsync(int purchaseInvoiceId)
        {
            if (purchaseInvoiceId < 1)
                return BadRequest("Invalid Id.");

            var list = await _service.GetItemsByPurchaseInvoiceIdAsync(purchaseInvoiceId);
            if (list.Count == 0)
                return NotFound("No items found for this invoice.");

            return Ok(list);
        }

        [HasPermission(PermissionModules.PurchaseInvoiceItems, PermissionAction.Create)]
        [HttpPost("AddItem", Name = "AddPurchaseInvoiceItemAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddPurchaseInvoiceItemAsync([FromBody] CreatePurchaseInvoiceItemDTO dto)
        {
            if (dto == null ||
                dto.PurchaseInvoiceId < 1 ||
                dto.ProductId < 1 ||
                dto.Quantity < 1 ||
                dto.Cost < 0)
                return BadRequest("Invalid Data.");

            try
            {
                var result = await _service.AddPurchaseInvoiceItemAsync(dto);

                return CreatedAtRoute(
                    "GetPurchaseInvoiceItemByIdAsync",
                    new { purchaseInvoiceItemId = result.PurchaseInvoiceItemId },
                   $"Done added purcase invoice item succesfully with id: {result}");
            }
            catch (SqlException ex) when (ex.Number==50002)
            {
                return BadRequest(ex.Message);
            }
        }

        [HasPermission(PermissionModules.PurchaseInvoiceItems, PermissionAction.Update)]
        [HttpPut("UpdateItem", Name = "UpdatePurchaseInvoiceItemAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdatePurchaseInvoiceItemAsync([FromForm] UpdatePurchaseInvoiceItemDTO dto)
        {
            if (dto == null || dto.PurchaseInvoiceItemId < 1|| dto.ProductId < 1 ||
                dto.Quantity < 1 || dto.Cost < 0)
                return BadRequest("Invalid Data.");

            if (!await _service.IsPurchaseInvoiceItemExistAsync(dto.PurchaseInvoiceItemId))
                return NotFound("Purchase invoice item not found.");


            try
            {
                bool result = await _service.UpdatePurchaseInvoiceItemAsync(dto);
                if (!result)
                    return NotFound("Purchase invoice item not found!");

                return Ok("Purchase invoice item updated successfully.");
            }
            catch (SqlException ex) when (ex.Number==50005)
            {
                return BadRequest(ex.Message);
            }
        }

        [HasPermission(PermissionModules.PurchaseInvoiceItems, PermissionAction.Delete)]
        [HttpDelete("Delete/{purchaseInvoiceItemId}", Name = "DeletePurchaseInvoiceItemAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePurchaseInvoiceItemAsync(int purchaseInvoiceItemId)
        {
            if (purchaseInvoiceItemId < 1)
                return BadRequest("Invalid Id.");

            try
            {
                bool result = await _service.DeletePurchaseInvoiceItemAsync(purchaseInvoiceItemId);
                if (!result)
                    return NotFound("Purchase invoice item not found.");

                return Ok("Purchase invoice item deleted successfully.");
            }
            catch (SqlException ex) when (ex.Number==50007)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
