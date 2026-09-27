using ERP.API.Authorization;
using ERP.Application.Interfaces.Servicies;
using ERP.Application.Servicies;
using ERP.Contacts.Requests.ProductWarehouses;
using ERP.Contacts.Responses;
using ERP.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [Authorize]
    [Route("api/ProductWarehouses")]
    [ApiController]
    public class ProductWarehousesController : ControllerBase
    {
        private readonly IProductWarehousesService
            _productWarehousesService;

        public ProductWarehousesController(
            IProductWarehousesService
                productWarehousesService)
        {
            _productWarehousesService =
                productWarehousesService;
        }

        [HasPermission( PermissionModules.ProductWarehouses, PermissionAction.Read)]
        [HttpGet("AllProductWarehouses", Name = "GetAllProductWarehousesAsync")]
        public async Task<ActionResult< IEnumerable<ProductWarehouseResponseDTO>>> GetAllProductWarehousesAsync()
        {
            var list = await _productWarehousesService.GetAllProductWarehousesAsync();

            if (list.Count == 0)
                return NotFound("No product warehouse records found.");

            return Ok(list);
        }

        [HasPermission( PermissionModules.ProductWarehouses, PermissionAction.Read)]
        [HttpGet("GetProductWarehouse/{productId}/{warehouseId}",Name = "GetProductWarehouseAsync")]
        public async Task<ActionResult> GetProductWarehouseAsync(int productId,int warehouseId)
        {
            if (productId < 1 || warehouseId < 1)
                return BadRequest("Invalid Data");

            var result =await _productWarehousesService.GetProductWarehouseAsync( productId, warehouseId);

            if (result == null)
                return NotFound("Product warehouse record not found.");

            return Ok(result);
        }

        [HasPermission( PermissionModules.ProductWarehouses, PermissionAction.Read)]
        [HttpGet("GetProductsInWarehouse/{warehouseId}",Name = "GetProductsInWarehouseAsync")]
        public async Task<ActionResult< IEnumerable<ProductWarehouseResponseDTO>>> GetProductsInWarehouseAsync(int warehouseId)
        {
            if ( warehouseId < 1)
                return BadRequest("Invalid Data");

            var result =await _productWarehousesService. GetProductsInWarehouseAsync( warehouseId);

            if (result.Count == 0)
                return NotFound("This Warehouse not found.");

            return Ok(result);
        }

        [HasPermission( PermissionModules.ProductWarehouses, PermissionAction.Read)]
        [HttpGet("GetProductInAllWarehouses/{productId}",Name = "GetProductInAllWarehousesAsync")]
        public async Task<ActionResult< IEnumerable<ProductWarehouseResponseDTO>>>  GetProductInAllWarehousesAsync(int productId)
        {
            if (productId< 1)
                return BadRequest("Invalid Data");

            var result =await _productWarehousesService.  GetProductInAllWarehousesAsync(productId);

            if (result.Count == 0)
                return NotFound("This product not found in any warehouse.");

            return Ok(result);
        }

        [HasPermission( PermissionModules.ProductWarehouses,PermissionAction.Create)]
        [HttpPost( "Add", Name = "AddProductWarehouseAsync")]
        public async Task<ActionResult> AddProductWarehouseAsync([FromBody] CreateProductWarehouseDTO dto)
        {
            if (dto == null ||dto.ProductId < 1 ||dto.WarehouseId < 1 ||dto.Quantity < 0)
            {
                return BadRequest("Invalid Data");
            }

            if (await _productWarehousesService.IsProductWarehouseExistAsync( dto.ProductId,dto.WarehouseId))
            {
                return BadRequest("This product already exists in this warehouse.");
            }

            bool result = await _productWarehousesService.AddProductWarehouseAsync(dto);

            if (!result)
                return BadRequest("Product warehouse could not be added.");

            return Ok("Product added to warehouse successfully.");
        }

        [HasPermission( PermissionModules.ProductWarehouses,PermissionAction.Update)]
        [HttpPut("Update",Name = "UpdateProductWarehouseAsync")]
        public async Task<ActionResult>UpdateProductWarehouseAsync( [FromForm] UpdateProductWarehouseDTO dto)
        {
            if (dto == null || dto.ProductId < 1 || dto.WarehouseId < 1 || dto.Quantity < 0)
            {
                return BadRequest("Invalid Data");
            }

            bool result = await _productWarehousesService .UpdateProductWarehouseAsync(dto);

            if (!result)
                return NotFound( "Product warehouse record not found.");

            return Ok("Product warehouse updated successfully.");
        }

        [HasPermission( PermissionModules.ProductWarehouses,PermissionAction.Delete)]
        [HttpDelete("Delete/{productId}/{warehouseId}",Name = "DeleteProductWarehouseAsync")]
        public async Task<ActionResult>DeleteProductWarehouseAsync( int productId, int warehouseId)
        {
            if (productId < 1 || warehouseId < 1)
                return BadRequest("Invalid Data");

            bool result = await _productWarehousesService.DeleteProductWarehouseAsync( productId, warehouseId);

            if (!result)
                return NotFound( "Product warehouse record not found.");

            return Ok( "Product warehouse deleted successfully.");
        }

        [HasPermission( PermissionModules.ProductWarehouses,PermissionAction.Read)]
        [HttpGet("exists/{productId}/{warehouseId}", Name = "IsProductWarehouseExistAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult>IsProductWarehouseExistAsync(int productId,int warehouseId)
        {
            bool isFound =await _productWarehousesService.IsProductWarehouseExistAsync(productId, warehouseId);

            if (!isFound)
                return NotFound("Product not found in this Warehouse.");

            return Ok(isFound);
        }

    }
}