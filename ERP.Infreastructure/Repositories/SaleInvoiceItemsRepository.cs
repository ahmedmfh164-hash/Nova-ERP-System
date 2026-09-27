using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;

namespace ERP.Infreastructure.Repositories;

public class SaleInvoiceItemsRepository : ISaleInvoiceItemsRepository
{
    private readonly IDBConnectionFactory _DbConnectionFactory;
    private readonly IStoredProcedtureExecutor _StoredProcedure;

    public SaleInvoiceItemsRepository(
        IDBConnectionFactory dbConnectionFactory,
        IStoredProcedtureExecutor storedProcedure)
    {
        _DbConnectionFactory = dbConnectionFactory;
        _StoredProcedure = storedProcedure;
    }

    public async Task<List<SaleInvoiceItem>> GetAllSaleInvoiceItemsAsync()
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_GetAllSaleInvoiceItems", con);

        var list = await _StoredProcedure.ExecuteListAsync(cmd, con,MapItem);

        return list.ToList();
    }

    public async Task<SaleInvoiceItem?> GetSaleInvoiceItemByIdAsync(int saleInvoiceItemId)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_GetSaleInvoiceItemById", con);

        SqlCommandExtentions.AddParameters(cmd,"@SaleInvoiceItemId", saleInvoiceItemId);

        var result= await _StoredProcedure.ExecuteSingleAsync(cmd,con,MapItem);

        return result;
    }

    public async Task<List<SaleInvoiceItem>> GetItemsBySaleInvoiceIdAsync( int saleInvoiceId)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_GetSaleInvoiceItemsByInvoiceId", con);

      SqlCommandExtentions.AddParameters(cmd,"@SaleInvoiceId", saleInvoiceId);

        var result= await _StoredProcedure.ExecuteListAsync(cmd,con,MapItem);

        return result.ToList();
    }

    public async Task<int> AddSaleInvoiceItemAsync(SaleInvoiceItem item)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_AddSaleInvoiceItem", con);

         SqlCommandExtentions.AddParameters(cmd,"@SaleInvoiceId", item.SaleInvoiceId);
         SqlCommandExtentions.AddParameters(cmd,"@ProductId", item.ProductId);
         SqlCommandExtentions.AddParameters(cmd,"@Quantity", item.Quantity);
         SqlCommandExtentions.AddParameters(cmd,"@Price", item.Price);

        return Convert.ToInt32(await _StoredProcedure.ExecuteScalarAsync(cmd,con));
    }

    public async Task<bool> EditSaleInvoiceItemAsync(SaleInvoiceItem item)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_UpdateSaleInvoiceItem", con);

         SqlCommandExtentions.AddParameters(cmd,"@SaleInvoiceItemId", item.SaleInvoiceItemId);
         SqlCommandExtentions.AddParameters(cmd,"@Quantity", item.Quantity);
         SqlCommandExtentions.AddParameters(cmd,"@Price", item.Price);

        return await _StoredProcedure.ExecuteBooleenAsync(cmd,con);
    }

    public async Task<bool> DeleteSaleInvoiceItemAsync(int saleInvoiceItemId)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand(
            "usp_DeleteSaleInvoiceItem", con);

         SqlCommandExtentions.AddParameters(cmd,"@SaleInvoiceItemId", saleInvoiceItemId);

        return await _StoredProcedure.ExecuteBooleenAsync(cmd,con);
    }

    public async Task<bool> IsSaleInvoiceItemExistAsync(int saleInvoiceItemId)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand( "usp_IsSaleInvoiceItemExistById", con);

        SqlCommandExtentions.AddParameters(cmd,"@SaleInvoiceItemId", saleInvoiceItemId);

        return await _StoredProcedure.ExecuteBooleenAsync(cmd,con);
    }

    private static SaleInvoiceItem MapItem(SqlDataReader reader)
    {
        return new SaleInvoiceItem(
            saleInvoiceItemId: reader.GetInt32(reader.GetOrdinal("SaleInvoiceItemId")),
            saleInvoiceId: reader.GetInt32(reader.GetOrdinal("SaleInvoiceId")),
            productId:reader.GetInt32(reader.GetOrdinal("ProductId")),
            quantity: reader.GetInt32(reader.GetOrdinal("Quantity")),
            price: reader.GetDecimal(reader.GetOrdinal("Price")),
            totalAmount: reader.GetDecimal(reader.GetOrdinal("TotalAmount")));
    }
}
