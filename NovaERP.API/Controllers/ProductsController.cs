using ERP.API.Authorization;
using ERP.Application.Interfaces.Services;
using ERP.Contacts.Requests.Pagination;
using ERP.Contacts.Requests.Products;
using ERP.Contacts.Responses;
using ERP.Core.Enums;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [Authorize]
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }


        [HasPermission(PermissionModules.Products, PermissionAction.Read)]
        [HttpGet("AllProducts", Name = "GetAllProductsAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetAllProductsAsync([FromQuery]PaginationRequestDTO dto)
        {
            if (dto.Page<1||dto.PageSize<1)
                return BadRequest("Invalid Data");

            List<ProductResponseDTO> productList = await _productsService.GetAllProductsAsync(dto);

            if (productList.Count == 0)
                return NotFound("No products found.");

            return Ok(productList);
        }


        [HasPermission(PermissionModules.Products, PermissionAction.Read)]
        [HttpGet("GetProduct/{ProductId}", Name = "GetProductByProductIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetProductByProductIdAsync(int ProductId)
        {
            if (ProductId < 1)
                return BadRequest("Invalid Data");

            var product =
                await _productsService.GetProductByProductIdAsync(ProductId);

            if (product == null)
                return NotFound("Product not found.");

            return Ok(product);
        }


        [HasPermission( PermissionModules.Products, PermissionAction.Create)]
        [HttpPost("AddProduct", Name = "AddProductAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddProductAsync( [FromBody] CreateProductDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid Data");

            var result = await _productsService.AddProductAsync(dto);

            return StatusCode(StatusCodes.Status201Created,$"Product Added Successfully with Id: {result}" );
        }


        [HasPermission( PermissionModules.Products, PermissionAction.Update)]
        [HttpPut("UpdateProduct", Name = "UpdateProductAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateProductAsync( [FromBody] UpdateProductDTO dto)
        {
            if (dto == null || dto.ProductId < 1)
                return BadRequest("Invalid Data");

            if (!await _productsService.IsProductExistAsync(dto.ProductId))
                return NotFound("This product is not found.");

            bool result =
                await _productsService.UpdateProductAsync(dto);

            if (!result)
                return NotFound("Product not found.");

            return Ok("Product updated successfully.");
        }


        [HasPermission( PermissionModules.Products, PermissionAction.Delete)]
        [HttpDelete("Delete/{productId}", Name = "DeleteProductAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteProductAsync(int productId)
        {
            if (productId < 1)
                return BadRequest("Invalid Id");

            bool result =
                await _productsService.DeleteProductAsync(productId);

            if (!result)
                return NotFound("This product is not found.");

            return Ok("Product deleted successfully.");
        }


        [HasPermission(PermissionModules.Products, PermissionAction.Read)]
        [HttpGet("exist/{productId}", Name = "IsProductExistByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsProductExistByIdAsync( int productId)
        {
            bool isFound =
                await _productsService.IsProductExistAsync(productId);

            if (!isFound)
                return NotFound("Product not found.");

            return Ok(isFound);
        }
    }
}