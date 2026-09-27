using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Core;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;

namespace ERP.Infreastructure.Repositories
{
    public class AuditLogsRepository : IAuditLogRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public AuditLogsRepository(IDBConnectionFactory dbConnectionFactory,
            IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }

        private AuditLog MapToAuditLog(SqlDataReader reader)
        {
            return new AuditLog
            (
                logId: reader.GetInt32(reader.GetOrdinal("LogId")),
                userId: reader.GetInt32(reader.GetOrdinal("UserId")),
                action: reader.GetString(reader.GetOrdinal("Action")),
                logDate: reader.GetDateTime(reader.GetOrdinal("LogDate"))
            );
        }

        public async Task<List<AuditLog>> GetAllAsync()
        {
            List<AuditLog> list = new List<AuditLog>();

            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_GetAllAuditLogs", con);

            list = await _StoredProcedture.ExecuteListAsync(cmd, con, MapToAuditLog);

            return list;
        }

        public async Task<List<AuditLog>> GetByUserIdAsync(int userId)
        {
            List<AuditLog> list = new List<AuditLog>();

            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_GetAuditLogsByUserId", con);

            SqlCommandExtentions.AddParameters(cmd, "@UserId", userId);

            list = await _StoredProcedture.ExecuteListAsync(cmd, con, MapToAuditLog);

            return list;
        }

        public async Task<int> AddAsync(AuditLog auditLog)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();

            await using var cmd = _StoredProcedture.CreateCommand("usp_AddAuditLog", con);

            SqlCommandExtentions.AddParameters(cmd, auditLog);

            int logId = await _StoredProcedture.ExecuteScalarAsync(cmd, con);

            return logId;
        }
    }
}
