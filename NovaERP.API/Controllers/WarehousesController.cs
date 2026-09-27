using ERP.API.Authorization;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.Warehouses;
using ERP.Contacts.Responses;
using ERP.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [Authorize]
    [Route("api/Warehouses")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehousesService _warehousesService;

        public WarehousesController(
            IWarehousesService warehousesService)
        {
            _warehousesService = warehousesService;
        }

        [HasPermission(PermissionModules.Warehouses, PermissionAction.Read)]
        [HttpGet("AllWarehouses", Name = "GetAllWarehousesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<
            IEnumerable<WarehouseResponseDTO>>> GetAllWarehousesAsync()
        {
            List<WarehouseResponseDTO> warehouseList = await _warehousesService .GetAllWarehousesAsync();

            if (warehouseList.Count == 0)
                return NotFound("No warehouses found.");

            return Ok(warehouseList);
        }

        [HasPermission( PermissionModules.Warehouses, PermissionAction.Read)]
        [HttpGet("GetWarehouse/{WarehouseId}", Name = "GetWarehouseByWarehouseIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetWarehouseByWarehouseIdAsync( int WarehouseId)
        {
            if (WarehouseId < 1)
                return BadRequest("Invalid Data");

            var warehouse = await _warehousesService .GetWarehouseByWarehouseIdAsync( WarehouseId);

            if (warehouse == null)
                return NotFound("Warehouse not found.");

            return Ok(warehouse);
        }

        [HasPermission( PermissionModules.Warehouses, PermissionAction.Create)]
        [HttpPost("AddWarehouse", Name = "AddWarehouseAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddWarehouseAsync( [FromBody] CreateWarehouseDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid Data");

            var result =await _warehousesService .AddWarehouseAsync(dto);

            return CreatedAtRoute(
                "GetWarehouseByWarehouseIdAsync",
                new
                {
                    WarehouseId = result.WarehouseId
                },
                result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateWarehouse", Name = "UpdateWarehouseAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateWarehouseAsync( [FromBody] UpdateWarehouseDTO dto)
        {
            if (dto == null || dto.WarehouseId < 1)
                return BadRequest("Invalid Data");

            if (!await _warehousesService.IsWarehouseExistAsync(dto.WarehouseId))
            {
                return NotFound("This warehouse is not found.");
            }

            bool result =await _warehousesService .UpdateWarehouseAsync(dto);

            if (!result)
                return NotFound("Warehouse not found.");

            return Ok("Warehouse updated successfully.");
        }

        [HasPermission( PermissionModules.Warehouses, PermissionAction.Delete)]
        [HttpDelete("Delete/{warehouseId}", Name = "DeleteWarehouseAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteWarehouseAsync( int warehouseId)
        {
            if (warehouseId < 1)
                return BadRequest("Invalid Id");

            bool result = await _warehousesService .DeleteWarehouseAsync(warehouseId);

            if (!result)  return NotFound( "This warehouse is not found.");

            return Ok("Warehouse deleted successfully.");
        }

        [HasPermission( PermissionModules.Warehouses, PermissionAction.Read)]
        [HttpGet("exist/{warehouseId}", Name = "IsWarehouseExistByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult>IsWarehouseExistByIdAsync(int warehouseId)
        {
            bool isFound =await _warehousesService.IsWarehouseExistAsync( warehouseId);

            if (!isFound)
                return NotFound("Warehouse not found.");

            return Ok(isFound);
        }

    }

}