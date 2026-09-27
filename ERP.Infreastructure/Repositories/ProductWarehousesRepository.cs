using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;

namespace ERP.Infreastructure.Repositories
{
    public class ProductWarehousesRepository : IProductWarehousesRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public ProductWarehousesRepository(
            IDBConnectionFactory dbConnectionFactory,
            IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }

        private ProductWarehouse MapToProductWarehouse(SqlDataReader reader)
        {
            return new ProductWarehouse(
                ProductId:reader.GetInt32( reader.GetOrdinal("ProductId")),
                WarehouseId: reader.GetInt32( reader.GetOrdinal("WarehouseId")),
                Quantity: reader.GetInt32( reader.GetOrdinal("Quantity"))
            );
        }

        public async Task<List<ProductWarehouse>> GetAllProductWarehousesAsync()
        {
            List<ProductWarehouse> list = new List<ProductWarehouse>();

            await using var con = await _DbConnectionFactory .CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_GetAllProductWarehouses", con);

            list = await _StoredProcedture.ExecuteListAsync( cmd, con, MapToProductWarehouse);

            return list;
        }

        public async Task<ProductWarehouse?> GetProductWarehouseAsync(int productId,int warehouseId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand( "usp_GetProductWarehouse", con);

            SqlCommandExtentions.AddParameters( cmd, "@ProductId", productId);
            SqlCommandExtentions.AddParameters( cmd, "@WarehouseId", warehouseId );

            ProductWarehouse productWarehouse = await _StoredProcedture.ExecuteSingleAsync( cmd, con, MapToProductWarehouse);

            return productWarehouse;
        }

         public async Task<List<ProductWarehouse>> GetProductInAllWarehousesAsync(int productId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand( "usp_GetProductInAllWarehouses", con);

            SqlCommandExtentions.AddParameters( cmd, "@ProductId", productId);

            List<ProductWarehouse> productWarehouses = await _StoredProcedture.ExecuteListAsync( cmd, con, MapToProductWarehouse);

            return productWarehouses;
        }

        public async Task<List<ProductWarehouse>> GetProductsInWarehouseAsync(int warehouseId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand( "usp_GetProductsInWarehouse", con);

            SqlCommandExtentions.AddParameters( cmd, "@WarehouseId", warehouseId );

           List<ProductWarehouse> productsWarehouse = await _StoredProcedture.ExecuteListAsync( cmd, con, MapToProductWarehouse);

           return productsWarehouse;
        }

        public async Task<bool> AddProductWarehouseAsync( ProductWarehouse productWarehouse)
        {
            await using var con = await _DbConnectionFactory .CreateConnectionAsync();

            await using var cmd =_StoredProcedture.CreateCommand( "usp_AddProductWarehouse", con);

            SqlCommandExtentions.AddParameters( cmd,productWarehouse);

            return await _StoredProcedture.ExecuteNonQueryAsync(cmd, con) > 0;
        }

        public async Task<bool> EditProductWarehouseAsync( ProductWarehouse productWarehouse)
        {
            await using var con = await _DbConnectionFactory .CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand( "usp_UpdateProductWarehouse", con);

            SqlCommandExtentions.AddParameters( cmd,productWarehouse);


            return await _StoredProcedture.ExecuteNonQueryAsync(cmd, con) > 0;
        }

        public async Task<bool> DeleteProductWarehouseAsync(int productId,int warehouseId)
        {
            await using var con = await _DbConnectionFactory .CreateConnectionAsync();

            await using var cmd =_StoredProcedture.CreateCommand("usp_DeleteProductWarehouse",con);

            SqlCommandExtentions.AddParameters( cmd, "@ProductId", productId );
            SqlCommandExtentions.AddParameters( cmd, "@WarehouseId", warehouseId );


            return await _StoredProcedture.ExecuteNonQueryAsync(cmd, con) > 0;
        }

        public async Task<bool> IsProductWarehouseExistAsync(int productId,int warehouseId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd =_StoredProcedture.CreateCommand( "usp_IsProductWarehouseExist", con);

          SqlCommandExtentions.AddParameters( cmd, "@ProductId", productId );
            SqlCommandExtentions.AddParameters( cmd, "@WarehouseId", warehouseId );

            return (await _StoredProcedture.ExecuteScalarAsync(cmd, con)>0);
        }
    }
}