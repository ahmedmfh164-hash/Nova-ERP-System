using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;

namespace ERP.Infreastructure.Repositories
{
    public class SaleReturnsRepository : ISaleReturnsRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedure;

        public SaleReturnsRepository( IDBConnectionFactory dbConnectionFactory,
            IStoredProcedtureExecutor storedProcedure)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedure = storedProcedure;
        }

        private SaleReturn MapSaleReturn(SqlDataReader Reader)
        {
            return new SaleReturn(
               returnId: Reader.GetInt32(Reader.GetOrdinal("ReturnId")),
               saleInvoiceId: Reader.GetInt32(Reader.GetOrdinal("SaleInvoiceId")),
               returnDate: Reader.GetDateTime(Reader.GetOrdinal("ReturnDate")),
               reasonReturn: Reader.GetString(Reader.GetOrdinal("ReasonReturn"))
            );
        }

        public async Task<List<SaleReturn>> GetAllSaleReturnsAsync()
        {
            using var con = await _DbConnectionFactory.CreateConnectionAsync();

            using var cmd = _StoredProcedure.CreateCommand("usp_GetAllSaleReturns", con);

            var list = await _StoredProcedure.ExecuteListAsync( cmd, con, MapSaleReturn);

            return list;
        }

        public async Task<SaleReturn?> GetSaleReturnByIdAsync(int returnId)
        {
            using var con = await _DbConnectionFactory.CreateConnectionAsync();

            using var cmd = _StoredProcedure.CreateCommand("usp_GetSaleReturnById", con);

            SqlCommandExtentions.AddParameters( cmd, "@ReturnId", returnId);

            var result =await _StoredProcedure.ExecuteSingleAsync( cmd, con, MapSaleReturn);

            return result;
        }

        public async Task<List<SaleReturn>>  GetSaleReturnsBySaleInvoiceIdAsync(int saleInvoiceId)
        {
            using var con =await _DbConnectionFactory.CreateConnectionAsync();

            using var cmd =_StoredProcedure.CreateCommand("usp_GetSaleReturnsBySaleInvoiceId", con);

            SqlCommandExtentions.AddParameters( cmd, "@SaleInvoiceId", saleInvoiceId);

            var list =await _StoredProcedure.ExecuteListAsync( cmd, con, MapSaleReturn);

            return list;
        }

        public async Task<int> AddSaleReturnAsync(SaleReturn saleReturn)
        {
            using var con = await _DbConnectionFactory.CreateConnectionAsync();

            using var cmd = _StoredProcedure.CreateCommand( "usp_AddSaleReturn", con);

            SqlCommandExtentions.AddParameters(cmd, saleReturn);

            var result = await _StoredProcedure.ExecuteScalarAsync( cmd, con);

            return result;
        }

        public async Task<bool> EditSaleReturnAsync(SaleReturn saleReturn)
        {
            using var con = await _DbConnectionFactory.CreateConnectionAsync();

            using var cmd = _StoredProcedure.CreateCommand( "usp_UpdateSaleReturn", con);

            SqlCommandExtentions.AddParameters( cmd, "@ReturnId", saleReturn.ReturnId);
            SqlCommandExtentions.AddParameters(cmd, "@ReasonReturn", saleReturn.ReasonReturn);

            var result = await _StoredProcedure.ExecuteBooleenAsync( cmd, con);

            return result;
        }

        public async Task<bool> DeleteSaleReturnAsync(int returnId)
        {
            using var con = await _DbConnectionFactory.CreateConnectionAsync();

            using var cmd = _StoredProcedure.CreateCommand( "usp_DeleteSaleReturn", con);

            SqlCommandExtentions.AddParameters(cmd, "@ReturnId", returnId);

            var result = await _StoredProcedure.ExecuteBooleenAsync(cmd, con);

            return result;
        }
    }
}
