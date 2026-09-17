using ERP.Application.Interfaces.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ERP.Infreastructure
{
    public class DbConnectionFactory : IDBConnectionFactory
    {
        private readonly string _connectionString = string.Empty;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString =
                 configuration.GetConnectionString("DefaultConnection")
                ?? throw new Exception("Connection string not found");
        }

        public async Task<SqlConnection> CreateConnectionAsync()
        {
            SqlConnection conn = new SqlConnection(_connectionString);

            await conn.OpenAsync();

            return conn;
        }
    }
}
