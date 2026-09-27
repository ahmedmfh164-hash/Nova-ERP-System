using ERP.API.Authorization;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.SaleReturns;
using ERP.Contacts.Responses;
using ERP.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleReturnItemsController : ControllerBase
    {
        private readonly ISaleReturnItemsService _SaleReturnItemsService;

        public SaleReturnItemsController(
            ISaleReturnItemsService saleReturnItemsService)
        {
            _SaleReturnItemsService = saleReturnItemsService;
        }

        [HttpGet("GetAll", Name = "GetAllSaleReturnItems")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Read)]
          [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SaleReturnItemResponseDTO>>> GetAllSaleReturnItems()
        {
            var result = await _SaleReturnItemsService.GetAllSaleReturnItemsAsync();
            
            if (result.Count==0)
                return NotFound("Sale return items not found.");

            return Ok(result);
        }

        [HttpGet("GetById/{ReturnItemId}", Name = "GetSaleReturnItemById")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Read)]
          [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetSaleReturnItemById(int ReturnItemId)
        {
            if (ReturnItemId<1)
                return BadRequest("Invalid Data");

            var result = await _SaleReturnItemsService.GetSaleReturnItemByIdAsync(ReturnItemId);

            if (result == null)
                return NotFound("Sale return item not found.");

            return Ok(result);
        }

        [HttpGet("GetByReturnId/{ReturnId}", Name = "GetSaleReturnItemsByReturnId")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Read)]
          [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SaleReturnItemResponseDTO>>> GetSaleReturnItemsByReturnId(int ReturnId)
        {
             if (ReturnId<1)
                return BadRequest("Invalid Data");

            var result =await _SaleReturnItemsService .GetItemsByReturnIdAsync(ReturnId);

            if (result.Count==0)
                return NotFound("Sale return items not found.");

            return Ok(result);
        }

        [HttpPost("Add", Name = "AddSaleReturnItem")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddSaleReturnItem(CreateSaleReturnItemDTO DTO)
        {
            if (DTO.ReturnId<1||DTO.SaleInvoiceItemId<1||DTO.Quantity<1)
                return BadRequest("Invalid Data");

            try
            {
                var result = await _SaleReturnItemsService.AddSaleReturnItemAsync(DTO);

                if (result <= 0)
                    return BadRequest("Failed to add sale return item.");

                return CreatedAtRoute("GetSaleReturnItemById", new { ReturnItemId = result }, $"Done added sale return item succesfully with id: {result}");
            }
            catch (SqlException ex) when (ex.Number==50004)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("Update", Name = "UpdateSaleReturnItem")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSaleReturnItem([FromForm] UpdateSaleReturnItemDTO DTO)
        {
            if (DTO.ReturnItemId<1||DTO.Quantity<1)
                return BadRequest("Invalid Data");

            try
            {
                var result = await _SaleReturnItemsService.EditSaleReturnItemAsync(DTO);

                if (!result)
                    return NotFound("Sale return item not found.");

                return Ok("Sale return item updated successfully.");
            }
            catch (SqlException ex) when (ex.Number==50004)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("Delete/{ReturnItemId}", Name = "DeleteSaleReturnItem")]
        [HasPermission(PermissionModules.Sales, PermissionAction.Delete)]
          [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSaleReturnItem( int ReturnItemId)
        {
             if (ReturnItemId<1)
                return BadRequest("Invalid Data");

            var result = await _SaleReturnItemsService.DeleteSaleReturnItemAsync(ReturnItemId);

            if (!result)
                return NotFound("Sale return item not found.");

            return Ok("Sale return item deleted successfully.");
        }
    }
}
