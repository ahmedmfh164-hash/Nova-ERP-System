using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Helpers;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;
using ERP.Application.Interfaces.Helpers;

namespace ERP.Infreastructure.Repositories
{
    public class PurchaseInvoiceItemsRepository : IPurchaseInvoiceItemsRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public PurchaseInvoiceItemsRepository(
            IDBConnectionFactory dbConnectionFactory,
            IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }

        private PurchaseInvoiceItem MapToPurchaseInvoiceItem(SqlDataReader reader)
        {
            return new PurchaseInvoiceItem(
                PurchaseInvoiceItemId: reader.GetInt32(reader.GetOrdinal("PurchaseInvoiceItemId")),
                PurchaseInvoiceId: reader.GetInt32(reader.GetOrdinal("PurchaseInvoiceId")),
                ProductId: reader.GetInt32(reader.GetOrdinal("ProductId")),
                Quantity: reader.GetInt32(reader.GetOrdinal("Quantity")),
                Cost: reader.GetDecimal(reader.GetOrdinal("Cost")),
                TotalAmount: reader.GetDecimal(reader.GetOrdinal("TotalAmount"))
            );
        }

        public async Task<List<PurchaseInvoiceItem>> GetAllPurchaseInvoiceItemsAsync()
        {
            List<PurchaseInvoiceItem> list = new List<PurchaseInvoiceItem>();
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_GetAllPurchaseInvoiceItems", con);
            list = await _StoredProcedture.ExecuteListAsync(cmd, con, MapToPurchaseInvoiceItem);
            return list;
        }

        public async Task<PurchaseInvoiceItem?> GetPurchaseInvoiceItemByIdAsync(int purchaseInvoiceItemId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_GetPurchaseInvoiceItemById", con);
            SqlCommandExtentions.AddParameters(cmd, "@PurchaseInvoiceItemId", purchaseInvoiceItemId);
            return await _StoredProcedture.ExecuteSingleAsync(cmd, con, MapToPurchaseInvoiceItem);
        }

        public async Task<List<PurchaseInvoiceItem>> GetItemsByPurchaseInvoiceIdAsync(int purchaseInvoiceId)
        {
            List<PurchaseInvoiceItem> list = new List<PurchaseInvoiceItem>();
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_GetPurchaseInvoiceItemsByInvoiceId", con);
            SqlCommandExtentions.AddParameters(cmd, "@PurchaseInvoiceId", purchaseInvoiceId);
            list = await _StoredProcedture.ExecuteListAsync(cmd, con, MapToPurchaseInvoiceItem);
            return list;
        }

        public async Task<int> AddPurchaseInvoiceItemAsync(PurchaseInvoiceItem item)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_AddPurchaseInvoiceItem", con);

            SqlCommandExtentions.AddParameters(cmd,item);

            return await _StoredProcedture.ExecuteScalarAsync(cmd, con);
        }

        public async Task<bool> EditPurchaseInvoiceItemAsync(PurchaseInvoiceItem item)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_UpdatePurchaseInvoiceItem", con);

            SqlCommandExtentions.AddParameters(cmd, "@PurchaseInvoiceItemId",item.PurchaseInvoiceItemId);
            SqlCommandExtentions.AddParameters(cmd, "@ProductId", item.ProductId);
            SqlCommandExtentions.AddParameters(cmd, "@Quantity", item.Quantity);
            SqlCommandExtentions.AddParameters(cmd, "@Cost", item.Cost);

            return await _StoredProcedture.ExecuteNonQueryAsync(cmd, con) > 0;
        }

        public async Task<bool> DeletePurchaseInvoiceItemAsync(int purchaseInvoiceItemId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_DeletePurchaseInvoiceItem", con);

            SqlCommandExtentions.AddParameters(cmd, "@PurchaseInvoiceItemId", purchaseInvoiceItemId);

            return await _StoredProcedture.ExecuteNonQueryAsync(cmd, con) > 0;
        }

        public async Task<bool> IsPurchaseInvoiceItemExistAsync(int purchaseInvoiceItemId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_IsPurchaseInvoiceItemExistById", con);

            SqlCommandExtentions.AddParameters(cmd, "@PurchaseInvoiceItemId", purchaseInvoiceItemId);

            return await _StoredProcedture.ExecuteBooleenAsync(cmd, con);
        }
    }
}
