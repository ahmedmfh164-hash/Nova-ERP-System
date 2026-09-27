using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;

namespace ERP.Infreastructure.Repositories
{
    public class SaleReturnItemsRepository : ISaleReturnItemsRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedure;

        public SaleReturnItemsRepository(IDBConnectionFactory dbConnectionFactory,
            IStoredProcedtureExecutor storedProcedure)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedure = storedProcedure;
        }

        private SaleReturnItem MapSaleReturnItem(SqlDataReader Reader)
        {
            return new SaleReturnItem(
               returnItemId: Reader.GetInt32(Reader.GetOrdinal("ReturnItemId")),
               returnId: Reader.GetInt32(Reader.GetOrdinal("ReturnId")),
               saleInvoiceItemId: Reader.GetInt32(Reader.GetOrdinal("SaleInvoiceItemId")),
               quantity: Reader.GetInt32(Reader.GetOrdinal("Quantity")),
               price: Reader.GetDecimal(Reader.GetOrdinal("Price")),
               totalAmount: Reader.GetDecimal(Reader.GetOrdinal("TotalAmount"))
            );
        }

        public async Task<List<SaleReturnItem>> GetAllSaleReturnItemsAsync()
        {
            using var con = await _DbConnectionFactory.CreateConnectionAsync();

            using var cmd = _StoredProcedure.CreateCommand("usp_GetAllSaleReturnItems", con);

            var list = await _StoredProcedure.ExecuteListAsync( cmd, con, MapSaleReturnItem);

            return list;
        }

        public async Task<SaleReturnItem?>GetSaleReturnItemByIdAsync(int returnItemId)
        {
            using var con =await _DbConnectionFactory.CreateConnectionAsync();

            using var cmd = _StoredProcedure.CreateCommand("usp_GetSaleReturnItemById", con);

            SqlCommandExtentions.AddParameters( cmd, "@ReturnItemId", returnItemId);

            var result =await _StoredProcedure.ExecuteSingleAsync( cmd, con, MapSaleReturnItem);

            return result;
        }

        public async Task<List<SaleReturnItem>>GetItemsByReturnIdAsync(int returnId)
        {
            using var con = await _DbConnectionFactory.CreateConnectionAsync();

            using var cmd = _StoredProcedure.CreateCommand( "usp_GetSaleReturnItemsByReturnId", con);

            SqlCommandExtentions.AddParameters( cmd, "@ReturnId", returnId);

            var list = await _StoredProcedure.ExecuteListAsync( cmd, con, MapSaleReturnItem);

            return list;
        }

        public async Task<int>AddSaleReturnItemAsync(SaleReturnItem saleReturnItem)
        {
            using var con = await _DbConnectionFactory.CreateConnectionAsync();

            using var cmd = _StoredProcedure.CreateCommand( "usp_AddSaleReturnItem", con);

            SqlCommandExtentions.AddParameters(cmd, "@ReturnId", saleReturnItem.ReturnId);
            SqlCommandExtentions.AddParameters(cmd, "@SaleInvoiceItemId", saleReturnItem.SaleInvoiceItemId);
            SqlCommandExtentions.AddParameters(cmd, "@Quantity", saleReturnItem.Quantity);

            var result = await _StoredProcedure.ExecuteScalarAsync( cmd, con);

            return result;
        }

        public async Task<bool> EditSaleReturnItemAsync(SaleReturnItem saleReturnItem)
        {
            using var con = await _DbConnectionFactory.CreateConnectionAsync();

            using var cmd =_StoredProcedure.CreateCommand("usp_UpdateSaleReturnItem", con);

            SqlCommandExtentions.AddParameters(cmd, "@ReturnItemId", saleReturnItem.ReturnItemId);
            SqlCommandExtentions.AddParameters(cmd, "@Quantity", saleReturnItem.Quantity);

            var result =
                await _StoredProcedure.ExecuteBooleenAsync( cmd, con);

            return result;
        }

        public async Task<bool>DeleteSaleReturnItemAsync(int returnItemId)
        {
            using var con = await _DbConnectionFactory.CreateConnectionAsync();

            using var cmd =_StoredProcedure.CreateCommand("usp_DeleteSaleReturnItem", con);

            SqlCommandExtentions.AddParameters(cmd, "@ReturnItemId", returnItemId);

            var result = await _StoredProcedure.ExecuteBooleenAsync(cmd, con);

            return result;
        }
    }
}
