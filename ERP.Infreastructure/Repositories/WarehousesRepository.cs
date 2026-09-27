using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Core;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;

namespace ERP.Infreastructure.Repositories
{
    public class WarehousesRepository : IWarehousesRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public WarehousesRepository(
            IDBConnectionFactory dbConnectionFactory,
            IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }

        private Warehouse MapToWarehouse(SqlDataReader reader)
        {
            return new Warehouse(
                WarehouseId: reader.GetInt32(reader.GetOrdinal("WarehouseId")),
                WarehouseName: reader.GetString(reader.GetOrdinal("WarehouseName")),
                Location: reader.GetString(reader.GetOrdinal("Location"))
            );
        }

        public async Task<List<Warehouse>> GetAllWarehousesAsync()
        {
            List<Warehouse> list = new List<Warehouse>();

            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_GetAllWarehouses", con);

            list = await _StoredProcedture.ExecuteListAsync(cmd, con, MapToWarehouse);

            return list;
        }

        public async Task<Warehouse?> GetWarehouseByWarehouseIdAsync( int warehouseId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand( "usp_GetWarehouseByWarehouseId",con);

            SqlCommandExtentions.AddParameters( cmd, "@WarehouseId", warehouseId);

            Warehouse warehouse = await _StoredProcedture.ExecuteSingleAsync( cmd, con,MapToWarehouse);

            return warehouse;
        }

        public async Task<int> AddWarehouseAsync( Warehouse warehouse)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand( "usp_AddNewWarehouse", con);

            SqlCommandExtentions.AddParameters( cmd, warehouse );

            warehouse.WarehouseId = await _StoredProcedture.ExecuteScalarAsync( cmd, con);

            return warehouse.WarehouseId;
        }

        public async Task<bool> EditWarehouseAsync( Warehouse warehouse)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand( "usp_UpdateWarehouse", con);

            SqlCommandExtentions.AddParameters( cmd, warehouse);

            return await _StoredProcedture.ExecuteNonQueryAsync( cmd, con) > 0;
        }

        public async Task<bool> DeleteWarehouseAsync( int warehouseId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand( "usp_DeleteWarehouse", con);

            SqlCommandExtentions.AddParameters( cmd, "@WarehouseId", warehouseId);

            return await _StoredProcedture.ExecuteNonQueryAsync( cmd, con) > 0;
        }

        public async Task<bool> IsWarehouseExistAsync( int warehouseId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand( "usp_IsWarehouseExistById",con);

            SqlCommandExtentions.AddParameters( cmd, "@WarehouseId", warehouseId);

            return await _StoredProcedture.ExecuteScalarAsync( cmd, con)>0;
        }
    }
}