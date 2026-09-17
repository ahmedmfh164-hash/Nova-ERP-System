using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Application.Interfaces.Repositories
{
    public interface ICategoriesRepository
    {
        public Task<List<Category>> GetCategoriesAsync();
        public Task<int?> AddCategoryAsync(string CategoryName);
        public Task<bool> EditCategoryAsync(Category category);
        public Task<bool?> DeleteCategoryAsync(int categoryId);
        public Task<Category?> GetCategoryByCategoryIdAsync(int categoryId);
        public Task<bool> isCategoryExistAsync(int categoryId);
        public Task<bool> isCategoryExistAsync(string CategoryName);



    }
}
