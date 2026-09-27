using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.Settings;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Application.Servicies
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _settings;

        public SettingsService(ISettingsRepository settings)
        {
            _settings = settings;
        }

        public async Task<List<SettingResponseDTO>> GetAllSettingsAsync()
        {
            var settings = await _settings.GetAllSettingsAsync();

            return settings.Select(x => x.ToResponseDTO()).ToList();
        }

        public async Task<SettingResponseDTO?> GetSettingByIdAsync(int SettingId)
        {
            var setting = await _settings.GetSettingByIdAsync(SettingId);

            if (setting == null)
                return null;

            return setting.ToResponseDTO();
        }

        public async Task<SettingResponseDTO> AddSettingAsync(CreateSettingDTO dto)
        {
            var setting = dto.ToEntity();
            setting.SettingId = await _settings.AddSettingAsync(setting);

            return setting.ToResponseDTO();
        }

        public async Task<bool> UpdateSettingAsync(UpdatedSettingDTO dto)
        {
            var setting = dto.ToEntity();
            return await _settings.UpdateSettingAsync(setting);
        }

        public async Task<bool> DeleteSettingAsync(int SettingId)
        {
            return await _settings.DeleteSettingAsync(SettingId);
        }

        public async Task<bool> IsSettingExistAsync(int SettingId)
        {
            return await _settings.IsSettingExistAsync(SettingId);
        }
    }
}
