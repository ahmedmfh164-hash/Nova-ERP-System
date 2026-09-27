using ERP.API.Authorization;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.PurchaseInvoices;
using ERP.Contacts.Responses;
using ERP.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace ERP.API.Controllers
{
    [Authorize]
    [Route("api/PurchaseInvoices")]
    [ApiController]
    public class PurchaseInvoicesController : ControllerBase
    {
        private readonly IPurchaseInvoicesService
            _purchaseInvoicesService;

        public PurchaseInvoicesController(IPurchaseInvoicesService purchaseInvoicesService)
        {
            _purchaseInvoicesService = purchaseInvoicesService;
        }

        [HasPermission(PermissionModules.Purchases, PermissionAction.Read)]
        [HttpGet("AllPurchaseInvoices", Name = "GetAllPurchaseInvoicesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PurchaseInvoiceResponseDTO>>> GetAllPurchaseInvoicesAsync()
        {
            var list = await _purchaseInvoicesService.GetAllPurchaseInvoicesAsync();

            if (list.Count == 0)
                return NotFound("No purchase invoices found.");

            return Ok(list);
        }

        [HasPermission(PermissionModules.Purchases, PermissionAction.Read)]
        [HttpGet("Get/{purchaseInvoiceId}", Name = "GetPurchaseInvoiceByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetPurchaseInvoiceByIdAsync(int purchaseInvoiceId)
        {
            if (purchaseInvoiceId < 1)
                return BadRequest("Invalid Data");

            var invoice = await _purchaseInvoicesService.GetPurchaseInvoiceByIdAsync(purchaseInvoiceId);

            if (invoice == null)
                return NotFound("Purchase invoice not found.");

            return Ok(invoice);
        }

        [HasPermission(PermissionModules.Purchases, PermissionAction.Create)]
        [HttpPost("Add", Name = "AddPurchaseInvoiceAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddPurchaseInvoiceAsync([FromBody] CreatePurchaseInvoiceDTO dto)
        {
            if (dto == null ||dto.SupplierId < 1)
            {
                return BadRequest("Invalid Data");
            }
            int UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = await _purchaseInvoicesService.AddPurchaseInvoiceAsync(UserId, dto);

            return CreatedAtRoute("GetPurchaseInvoiceByIdAsync",
                new { purchaseInvoiceId = result.PurchaseInvoiceId },$"Done added purchase invoice succesfully with id: {result}");
        }


        [HasPermission(PermissionModules.Purchases, PermissionAction.Update)]
        [HttpPut("Confirm/{purchaseInvoiceId}", Name = "ConfirmPurchaseInvoiceAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ConfirmPurchaseInvoiceAsync(int purchaseInvoiceId)
        {
            if (purchaseInvoiceId < 1)
                return BadRequest("Invalid Id");

            try
            {
                bool result = await _purchaseInvoicesService.ConfirmPurchaseInvoiceAsync(purchaseInvoiceId);

                if (!result)
                    return NotFound("Purchase invoice not found.");

                return Ok("Purchase invoice Confirmed successfully.");
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HasPermission(PermissionModules.Purchases, PermissionAction.Update)]
        [HttpPut("Cancel/{purchaseInvoiceId}", Name = "CancelPurchaseInvoiceAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CancelPurchaseInvoiceAsync(int purchaseInvoiceId)
        {
            if (purchaseInvoiceId < 1)
                return BadRequest("Invalid Id");

            try
            {
                bool result = await _purchaseInvoicesService.CancelPurchaseInvoiceAsync(purchaseInvoiceId);

                if (!result)
                    return NotFound("Purchase invoice not found.");

                return Ok("Purchase invoice Canceled successfully.");
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HasPermission(PermissionModules.Purchases, PermissionAction.Read)]
        [HttpGet("Exists/{PurchaseInvoiceId}", Name = "IsPurchaseInvoiceExistAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsPurchaseInvoiceExistAsync(int PurchaseInvoiceId)
        {
            if (PurchaseInvoiceId < 1)
            {
                return BadRequest("Invalid Data");
            }

            bool exists = await _purchaseInvoicesService.IsPurchaseInvoiceExistAsync(PurchaseInvoiceId);

            return Ok(exists ? "Purchase invoice Found" : NotFound("Purchase invoice not found."));
        }

    }
}