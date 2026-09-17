using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.Categories;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Application.Servicies
{
    public class CategoriesService:ICategoriesService
    {
        private readonly ICategoriesRepository _repo;
        private readonly IFileStorageService _fileStorageService;
        private readonly IDirectoryPathService _directoryPathService;
        public CategoriesService(ICategoriesRepository repo, IFileStorageService fileStorageService, IDirectoryPathService directoryPathService)
        {
            _repo = repo;
            _fileStorageService = fileStorageService;
            _directoryPathService = directoryPathService;
        }

        public async Task<List<CategoryResponseDTO>> GetCategoriesAsync()
        {
            List<Category> categories = await _repo.GetCategoriesAsync();

            return categories.Select(category=>category.ToResponseDTO()).ToList();

        }

        public async Task<int?> AddCategoryAsync(string categoryName)
        {
            int? categoryId =await _repo.AddCategoryAsync(categoryName);

            return categoryId;
        }


        public async Task<bool> UpdateCategoryAsync(UpdatedCategoryDTO updatedCategory)
        {
            bool isUpdated = false;

            isUpdated= await _repo.EditCategoryAsync(updatedCategory.ToEntity());

            return isUpdated;
        }

        public async Task<bool?> DeleteCategoryAsync(int CategoryId)
        {
            var isDeleted = await _repo.DeleteCategoryAsync(CategoryId);

            return isDeleted;
        }

        public async Task<CategoryResponseDTO?> GetCategoryByCategoryIdAsync(int categoryId)
        {
            var category = await _repo.GetCategoryByCategoryIdAsync(categoryId);

            return category == null ? null:category.ToResponseDTO();
        }

        public async Task<bool> IsCategoryExistAsync(int CategoryId)
        {
            return await _repo.isCategoryExistAsync(CategoryId);

        }

        public async Task<bool> IsCategoryExistAsync(string categoryName)
        {
            return await _repo.isCategoryExistAsync(categoryName);

        }


    }
}
