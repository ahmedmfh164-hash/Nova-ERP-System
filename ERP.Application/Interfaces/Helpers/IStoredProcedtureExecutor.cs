using Microsoft.Data.SqlClient;

namespace ERP.Application.Interfaces.Helpers
{
    public interface IStoredProcedtureExecutor
    {
        public Task<SqlConnection> CreateConnectionAsync();
        public SqlCommand CreateCommand(string spName, SqlConnection connection);
        public Task<List<T>> ExecuteListAsync<T>(SqlCommand cmd, SqlConnection conn,Func<SqlDataReader, T> mapFunc);
        public Task<T> ExecuteSingleAsync<T>(SqlCommand cmd, SqlConnection conn,
        Func<SqlDataReader, T> mapFunc);
        public Task<int> ExecuteScalarAsync(SqlCommand cmd, SqlConnection con);
        public Task<int> ExecuteNonQueryAsync(SqlCommand cmd, SqlConnection conn);
        public Task<bool> ExecuteBooleenAsync(SqlCommand cmd, SqlConnection conn);



    }
}
