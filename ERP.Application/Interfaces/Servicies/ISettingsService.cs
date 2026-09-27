using ERP.Contacts.Requests.Settings;
using ERP.Contacts.Responses;

namespace ERP.Application.Interfaces.Servicies
{
    public interface ISettingsService
    {
        Task<List<SettingResponseDTO>> GetAllSettingsAsync();
        Task<SettingResponseDTO?> GetSettingByIdAsync(int SettingId);
        Task<SettingResponseDTO> AddSettingAsync(CreateSettingDTO dto);
        Task<bool> UpdateSettingAsync(UpdatedSettingDTO dto);
        Task<bool> DeleteSettingAsync(int SettingId);
        Task<bool> IsSettingExistAsync(int SettingId);
    }
}
