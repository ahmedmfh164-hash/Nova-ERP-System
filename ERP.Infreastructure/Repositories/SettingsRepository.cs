using ERP.Application.Interfaces.Data;
using ERP.Application.Interfaces.Helpers;
using ERP.Application.Interfaces.Repositories;
using ERP.Domain.Entities;
using ERP.Infreastructure.Extentions;
using Microsoft.Data.SqlClient;

namespace ERP.Infreastructure.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly IDBConnectionFactory _DbConnectionFactory;
        private readonly IStoredProcedtureExecutor _StoredProcedture;

        public SettingsRepository(
            IDBConnectionFactory dbConnectionFactory,
            IStoredProcedtureExecutor storedProcedureExecutor)
        {
            _DbConnectionFactory = dbConnectionFactory;
            _StoredProcedture = storedProcedureExecutor;
        }

        private Setting MapToSetting(SqlDataReader reader)
        {
            return new Setting
            (
                settingId: reader.GetInt32(reader.GetOrdinal("SettingId")),
                settingName: reader.GetString(reader.GetOrdinal("SettingName")),
                settingValue: reader.IsDBNull(reader.GetOrdinal("SettingValue"))? null
                         : reader.GetString(reader.GetOrdinal("SettingValue"))
            );
        }

        public async Task<List<Setting>> GetAllSettingsAsync()
        {
            List<Setting> list = new List<Setting>();

            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_GetAllSettings", con);

            list = await _StoredProcedture.ExecuteListAsync(cmd, con, MapToSetting);

            return list;
        }

        public async Task<Setting?> GetSettingByIdAsync(int SettingId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_GetSettingById", con);

            SqlCommandExtentions.AddParameters(cmd, "@SettingId", SettingId);

            Setting setting = await _StoredProcedture.ExecuteSingleAsync(cmd, con, MapToSetting);

            return setting;
        }

        public async Task<int> AddSettingAsync(Setting setting)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_AddSetting", con);

            SqlCommandExtentions.AddParameters(cmd, setting);

            int SettingId = await _StoredProcedture.ExecuteScalarAsync(cmd, con);

            return SettingId;
        }

        public async Task<bool> UpdateSettingAsync(Setting setting)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_UpdateSetting", con);

            SqlCommandExtentions.AddParameters(cmd, setting);

            return await _StoredProcedture.ExecuteBooleenAsync(cmd, con);
        }

        public async Task<bool> DeleteSettingAsync(int SettingId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_DeleteSetting", con);

            SqlCommandExtentions.AddParameters(cmd, "@SettingId", SettingId);

            return await _StoredProcedture.ExecuteBooleenAsync(cmd, con);
        }

        public async Task<bool> IsSettingExistAsync(int SettingId)
        {
            await using var con = await _DbConnectionFactory.CreateConnectionAsync();
            await using var cmd = _StoredProcedture.CreateCommand("usp_IsSettingExistById", con);

            SqlCommandExtentions.AddParameters(cmd, "@SettingId", SettingId);

            bool isFound = await _StoredProcedture.ExecuteBooleenAsync(cmd, con);

            return isFound;
        }
    }
}
