using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Contacts.Requests.Products;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;

namespace ERP.Infreastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public ProductsRepository(
            IDBConnectionFactory dbConnectionFactory,
            IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }


        private Product MapToProduct(SqlDataReader reader)
        {
            return new Product
            (
                ProductId: reader.GetInt32( reader.GetOrdinal("ProductId")),

                ProductName: reader.GetString( reader.GetOrdinal("ProductName")),

                Price: reader.GetDecimal( reader.GetOrdinal("Price")),

                Cost: reader.GetDecimal( reader.GetOrdinal("Cost")),

                SupplierId: reader.GetInt32( reader.GetOrdinal("SupplierId")),

                CategoryId: reader.GetInt32( reader.GetOrdinal("CategoryId")),

                Quantity: reader.IsDBNull( reader.GetOrdinal("Quantity")) ? null
                    : reader.GetInt32(reader.GetOrdinal("Quantity"))
            );
        }


        public async Task<List<Product>> GetAllProductsAsync(Pagination pagination)
        {
            List<Product> list = new List<Product>();

            await using var con =
                await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand( "usp_GetAllProducts", con);

            SqlCommandExtentions.AddParameters(cmd, pagination);

            list = await _StoredProcedture.ExecuteListAsync(cmd, con,MapToProduct);

            return list;
        }


        public async Task<Product?> GetProductByProductIdAsync(int productId)
        {
            await using var con =
                await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand(
                    "usp_GetProductByProductId", con);

            SqlCommandExtentions.AddParameters( cmd,
                "@ProductId", productId);

            Product product =
                await _StoredProcedture.ExecuteSingleAsync(
                    cmd, con, MapToProduct);

            return product;
        }


        public async Task<int> AddProductAsync(Product product)
        {
            await using var con =
                await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_AddNewProduct", con);

            SqlCommandExtentions.AddParameters( cmd,  product);

            product.ProductId = await _StoredProcedture.ExecuteScalarAsync( cmd, con);

            return product.ProductId;
        }


        public async Task<bool> EditProductAsync(UpdateProductDTO product)
        {
            await using var con =
                await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand(
                    "usp_UpdateProduct", con);

            SqlCommandExtentions.AddParameters( cmd, product);

            return await _StoredProcedture.ExecuteBooleenAsync( cmd, con);
        }


        public async Task<bool> DeleteProductAsync(int productId)
        {
            await using var con =
                await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd =_StoredProcedture.CreateCommand(
                    "usp_DeleteProduct",con);

            SqlCommandExtentions.AddParameters( cmd,
                "@ProductId",productId);

            return await _StoredProcedture.ExecuteBooleenAsync( cmd,con) ;
        }


        public async Task<bool> IsProductExistAsync(int productId)
        {
            await using var con =
                await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd =
                _StoredProcedture.CreateCommand(
                    "usp_IsProductExistById", con);

            SqlCommandExtentions.AddParameters( cmd,
                "@ProductId", productId);

            return await _StoredProcedture.ExecuteBooleenAsync( cmd, con);
        }
    }
}