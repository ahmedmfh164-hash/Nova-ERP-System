using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.Categories;
using ERP.Contacts.Requests.People;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _service;

        public CategoriesController(ICategoriesService service)
        {
            _service = service;
        }



        [HttpGet("Categories", Name = "GetCategoriesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoryResponseDTO>>> GetCategoriesAsync()
        {
            List<CategoryResponseDTO> categories = await _service.GetCategoriesAsync();

            if (categories.Count == 0)
                return NotFound("No categories found.");

            return Ok(categories);
        }


        [HttpGet("GetCategories/{CategoryId}", Name = "GetCategoriesByCategoriesIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCategoriesByCategoriesIdAsync(int CategoryId)
        {
            if (CategoryId < 1)
                return BadRequest("Invalid Data");

            var category = await _service.GetCategoryByCategoryIdAsync(CategoryId);

            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        [HttpPost("AddCategory", Name = "AddCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddCategoryAsync([FromForm] string CategoryName)
        {
            if (string.IsNullOrEmpty(CategoryName))
            {
                return BadRequest("Invalid Data");
            }
            var result = await _service.AddCategoryAsync(CategoryName);

            return Ok($"Done created category successfully with Id : {result}");
        }


        [HttpPatch("UpdateInfo", Name = "UpdateCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateCategoryAsync([FromForm] UpdatedCategoryDTO updatedCategory)
        {

            if (updatedCategory == null||string.IsNullOrEmpty(updatedCategory.CategoryName))
            {
                return BadRequest("Invalid Data");
            }

            return await _service.UpdateCategoryAsync(updatedCategory) ? Ok("Category updated successfully") : NotFound("Category not found");
        }


        [HttpDelete("Delete/{CategoryId}", Name = "DeleteCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCategoryAsync(int CategoryId)
        {
            if (CategoryId<1)
                return BadRequest("Invalid Id");

            if (await _service.DeleteCategoryAsync(CategoryId)!=null)
                return Ok("Done delete category successfully.");
            else
                return NotFound("This category is not found!");

        }


        [HttpGet("exist/{CategoryId}", Name = "IsCategoryExistByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsCategoryExistByIdAsync(int CategoryId)
        {
            bool isFound = await _service.IsCategoryExistAsync(CategoryId);

            if (!isFound)
                return NotFound("Not Found");

            return Ok(isFound);
        }

        [HttpGet("exists/{CategoryName}", Name = "IsNameExistByNameAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsCategoryExistByNameAsync(int CategoryName)
        {
            bool isFound = await _service.IsCategoryExistAsync(CategoryName);

            if (!isFound)
                return NotFound("Not Found");

            return Ok(isFound);
        }

    }
}
