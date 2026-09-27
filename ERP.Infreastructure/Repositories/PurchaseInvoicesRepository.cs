using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Core;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;

namespace ERP.Infreastructure.Repositories
{
    public class PurchaseInvoicesRepository : IPurchaseInvoicesRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public PurchaseInvoicesRepository(
            IDBConnectionFactory dbConnectionFactory,
            IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }

        private PurchaseInvoice MapToPurchaseInvoice(SqlDataReader reader)
        {
            return new PurchaseInvoice(
                PurchaseInvoiceId: reader.GetInt32( reader.GetOrdinal("PurchaseInvoiceId")),
                SupplierId: reader.GetInt32( reader.GetOrdinal("SupplierId")),
                CreatedByUserId: reader.GetInt32( reader.GetOrdinal("CreatedByUserId")),
                InvoiceDate: reader.GetDateTime( reader.GetOrdinal("InvoiceDate")),
                TotalAmount: reader.GetDecimal( reader.GetOrdinal("TotalAmount")),
                Status:reader.GetByte( reader.GetOrdinal("Status"))
            );
        }

        public async Task<List<PurchaseInvoice>> GetAllPurchaseInvoicesAsync()
        {
            List<PurchaseInvoice> list = new List<PurchaseInvoice>();

            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_GetAllPurchaseInvoices",con);

            list = await _StoredProcedture.ExecuteListAsync( cmd, con, MapToPurchaseInvoice);

            return list;
        }

        public async Task<PurchaseInvoice?> GetPurchaseInvoiceByIdAsync( int purchaseInvoiceId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand( "usp_GetPurchaseInvoiceById", con);

            SqlCommandExtentions.AddParameters( cmd, "@PurchaseInvoiceId", purchaseInvoiceId);

            PurchaseInvoice invoice = await _StoredProcedture.ExecuteSingleAsync( cmd, con, MapToPurchaseInvoice);

            return invoice;
        }

        public async Task<int> AddPurchaseInvoiceAsync(PurchaseInvoice purchaseInvoice)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd =_StoredProcedture.CreateCommand("usp_AddPurchaseInvoice",con);

            SqlCommandExtentions.AddParameters( cmd, purchaseInvoice);

            return await _StoredProcedture.ExecuteScalarAsync(cmd, con);
        }

         public async Task<bool> ConfirmPurchaseInvoiceAsync(int purchaseInvoiceId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd =_StoredProcedture.CreateCommand("usp_ConfirmPurchaseInvoice",con);

            SqlCommandExtentions.AddParameters( cmd, "@PurchaseInvoiceId", purchaseInvoiceId);

            return await _StoredProcedture.ExecuteBooleenAsync(cmd,con);
        }

         public async Task<bool> CancelPurchaseInvoiceAsync(int purchaseInvoiceId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd =_StoredProcedture.CreateCommand("usp_CancelPurchaseInvoice",con);

            SqlCommandExtentions.AddParameters( cmd, "@PurchaseInvoiceId", purchaseInvoiceId);

            return await _StoredProcedture.ExecuteBooleenAsync(cmd, con);
        }
     
        public async Task<bool> IsPurchaseInvoiceExistAsync(int purchaseInvoiceId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd =_StoredProcedture.CreateCommand("usp_IsPurchaseInvoiceExistById",con);

            SqlCommandExtentions.AddParameters( cmd, "@PurchaseInvoiceId", purchaseInvoiceId);

            return await _StoredProcedture.ExecuteBooleenAsync(cmd, con);
        }
    }
}