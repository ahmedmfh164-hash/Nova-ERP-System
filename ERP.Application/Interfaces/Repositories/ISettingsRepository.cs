using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories
{
    public interface ISettingsRepository
    {
        Task<List<Setting>> GetAllSettingsAsync();
        Task<Setting?> GetSettingByIdAsync(int SettingId);
        Task<int> AddSettingAsync(Setting setting);
        Task<bool> UpdateSettingAsync(Setting setting);
        Task<bool> DeleteSettingAsync(int SettingId);
        Task<bool> IsSettingExistAsync(int SettingId);
    }
}
