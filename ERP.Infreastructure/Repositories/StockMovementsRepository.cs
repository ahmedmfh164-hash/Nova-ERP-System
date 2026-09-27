using Microsoft.Data.SqlClient;
using ERP.Application.Interfaces.Repositories;
using ERP.Domain.Entities;
using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Infreastructure.Extentions;
using System.Data;

namespace ERP.Infreastructure.Repositories;

public class StockMovementsRepository : IStockMovementsRepository
{
    private readonly IDBConnectionFactory _DbConnectionFactory;
    private readonly IStoredProcedtureExecutor _StoredProcedure;

    public StockMovementsRepository(
        IDBConnectionFactory dbConnectionFactory,
        IStoredProcedtureExecutor storedProcedure)
    {
        _DbConnectionFactory = dbConnectionFactory;
        _StoredProcedure = storedProcedure;
    }

    public async Task<List<StockMovement>> GetAllStockMovementsAsync()
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_GetAllStockMovements", con);

      var list = await _StoredProcedure.ExecuteListAsync( cmd, con, MapStockMovement);

            return list;
    }

    public async Task<StockMovement?> GetStockMovementByIdAsync(int movementId)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_GetStockMovementById", con);

        SqlCommandExtentions.AddParameters(cmd,"@MovementId", movementId);

        var result =await _StoredProcedure.ExecuteSingleAsync(cmd, con, MapStockMovement);

        return result;
    }

    public async Task<int> AddStockMovementAsync(StockMovement movement)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_AddStockMovement", con);

        SqlCommandExtentions.AddParameters(cmd,movement);

        var result = await _StoredProcedure.ExecuteScalarAsync(cmd,con);

        return result;
    }

    private static StockMovement MapStockMovement(SqlDataReader reader)
    {
        return new StockMovement(
            reader.GetInt32(reader.GetOrdinal("MovementId")),
            reader.GetInt32(reader.GetOrdinal("ProductId")),
            reader.GetByte(reader.GetOrdinal("MovementType")),
            reader.GetInt32(reader.GetOrdinal("Quantity")),
            reader.GetDateTime(reader.GetOrdinal("MovementDate")),
            reader.GetInt32(reader.GetOrdinal("ReferenceId")));
    }
}
