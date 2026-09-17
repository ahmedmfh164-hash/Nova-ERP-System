using ERP.Contacts.Requests.Categories;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Application.Interfaces.Servicies
{
    public interface ICategoriesService
    {
        public Task<List<CategoryResponseDTO>> GetCategoriesAsync();
        public Task<int?> AddCategoryAsync(string CategoryName);
        public Task<bool> UpdateCategoryAsync(UpdatedCategoryDTO updatedCategory);
        public Task<bool?> DeleteCategoryAsync(int categoryId);
        public Task<CategoryResponseDTO?> GetCategoryByCategoryIdAsync(int categoryId);
        public Task<bool> IsCategoryExistAsync(int categoryId);
        public Task<bool> IsCategoryExistAsync(string CategoryName);

    }
}
