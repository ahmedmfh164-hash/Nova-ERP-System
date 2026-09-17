using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;
using ERP.Application.Interfaces.Repositories;

namespace ERP.Infreastructure.Repositories
{
    public class CategoriesRepository:ICategoriesRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public CategoriesRepository(IDBConnectionFactory dbConnectionFactory,
        IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }

        private Category MapToCategory(SqlDataReader reader)
        {
            return new Category
            (
                categoryId: reader.GetInt32(reader.GetOrdinal("CategoryId")),
                categoryName: reader.GetString(reader.GetOrdinal("CategoryName"))
            );
        }

   
        public async Task<List<Category>> GetCategoriesAsync()
        {
            List<Category> list = new List<Category>();

            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_GetCategories", con);

            list= await _StoredProcedture.ExecuteListAsync(cmd, con, MapToCategory);

            return list;
        }

        public async Task<int?> AddCategoryAsync(string CategoryName)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_AddCategory", con);

            SqlCommandExtentions.AddParameters(cmd,"@CategoryName",CategoryName);

            int categoryId = await _StoredProcedture.ExecuteScalarAsync(cmd, con);

            return categoryId;
        }

        public async Task<bool> EditCategoryAsync(Category category)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_UpdateCategory", con);

            SqlCommandExtentions.AddParameters(cmd,category);

            return (await _StoredProcedture.ExecuteNonQueryAsync(cmd, con)>0);

        }

        public async Task<bool?> DeleteCategoryAsync(int categoryId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_DeleteCategory", con);

            SqlCommandExtentions.AddParameters(cmd, "@CategoryId", categoryId);

            bool? isDeleted = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isDeleted;
        }


        public async Task<Category?> GetCategoryByCategoryIdAsync(int categoryId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_GetCategoryByCategoryId", con);

            SqlCommandExtentions.AddParameters(cmd, "@CategoryId", categoryId);

            Category category = await _StoredProcedture.ExecuteSingleAsync(cmd, con, MapToCategory);

            return category;
        }


        public async Task<bool> isCategoryExistAsync(int categoryId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_isCategoryExistbyId", con);

            SqlCommandExtentions.AddParameters(cmd, "@CategoryId", categoryId);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }

        public async Task<bool> isCategoryExistAsync(string CategoryName)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_isCategoryExistByName", con);

            SqlCommandExtentions.AddParameters(cmd, "@CategoryName", CategoryName);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }




    }
}
