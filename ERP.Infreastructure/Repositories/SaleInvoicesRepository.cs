using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ERP.Infreastructure.Repositories;

public class SaleInvoicesRepository : ISaleInvoicesRepository
{
    private readonly IDBConnectionFactory _DbConnectionFactory;
    private readonly IStoredProcedtureExecutor _StoredProcedure;

    public SaleInvoicesRepository(
        IDBConnectionFactory dbConnectionFactory,
        IStoredProcedtureExecutor storedProcedure)
    {
        _DbConnectionFactory = dbConnectionFactory;
        _StoredProcedure = storedProcedure;
    }

    public async Task<List<SaleInvoice>> GetAllSaleInvoicesAsync()
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand( "usp_GetAllSaleInvoices", con);

      var list = await _StoredProcedure.ExecuteListAsync(cmd, con,MapSaleInvoice);

        return list;
    }

    public async Task<SaleInvoice?> GetSaleInvoiceByIdAsync(int saleInvoiceId)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_GetSaleInvoiceById", con);

        SqlCommandExtentions.AddParameters(cmd,"@SaleInvoiceId", saleInvoiceId);

       var ressult= await _StoredProcedure.ExecuteSingleAsync(cmd, con, MapSaleInvoice);
        return ressult;
    }

    public async Task<int> AddSaleInvoiceAsync(SaleInvoice invoice)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_AddSaleInvoice", con);

         SqlCommandExtentions.AddParameters(cmd,"@CustomerId", invoice.CustomerId);
         SqlCommandExtentions.AddParameters(cmd,"@CreatedByUserId", invoice.CreatedByUserId);

        var result = await _StoredProcedure.ExecuteScalarAsync(cmd,con);

        return result;
    }

    public async Task<bool> DeleteSaleInvoiceAsync(int saleInvoiceId)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_DeleteSaleInvoice", con);

        SqlCommandExtentions.AddParameters(cmd,"@SaleInvoiceId", saleInvoiceId);

        return await _StoredProcedure.ExecuteBooleenAsync(cmd,con);
    }

    public async Task<bool> IsSaleInvoiceExistAsync(int saleInvoiceId)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_IsSaleInvoiceExistById", con);

         SqlCommandExtentions.AddParameters(cmd,"@SaleInvoiceId", saleInvoiceId);

        return await _StoredProcedure.ExecuteBooleenAsync(cmd,con);
    }

    public async Task<bool> ConfirmSaleInvoiceAsync(int saleInvoiceId)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand( "usp_ConfirmSaleInvoice", con);

         SqlCommandExtentions.AddParameters(cmd,"@SaleInvoiceId", saleInvoiceId);

        return await _StoredProcedure.ExecuteBooleenAsync(cmd,con);
    }

    public async Task<bool> CancelSaleInvoiceAsync(int saleInvoiceId)
    {
        using var con = await _DbConnectionFactory.CreateConnectionAsync();

        using var cmd = _StoredProcedure.CreateCommand("usp_CancelSaleInvoice", con);

         SqlCommandExtentions.AddParameters(cmd,"@SaleInvoiceId", saleInvoiceId);

        return await _StoredProcedure.ExecuteBooleenAsync(cmd,con);
    }

    private static SaleInvoice MapSaleInvoice(SqlDataReader reader)
    {
        return new SaleInvoice(
           saleInvoiceId: reader.GetInt32(reader.GetOrdinal("SaleInvoiceId")),
           customerId: reader.GetInt32(reader.GetOrdinal("CustomerId")),
           createdByUserId: reader.GetInt32(reader.GetOrdinal("CreatedByUserId")),
           invoiceDate: reader.GetDateTime(reader.GetOrdinal("InvoiceDate")),
           totalAmount: reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
           status: reader.GetByte(reader.GetOrdinal("Status")));
    }
}
