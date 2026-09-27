using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.Settings;
using ERP.Contacts.Responses;
using Microsoft.AspNetCore.Mvc;
using ERP.API.Authorization;
using ERP.Core.Enums;

namespace ERP.API.Controllers
{
    [Route("api/Settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settings;

        public SettingsController(ISettingsService settings)
        {
            _settings = settings;
        }

        [HttpGet("AllSettings", Name = "GetAllSettingsAsync")]
        [HasPermission(PermissionModules.Settings, PermissionAction.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<SettingResponseDTO>>> GetAllSettingsAsync()
        {
            List<SettingResponseDTO> settingsList = await _settings.GetAllSettingsAsync();

            if (settingsList.Count == 0)
                return NotFound("No settings found.");

            return Ok(settingsList);
        }

        [HttpGet("GetSetting/{SettingId}", Name = "GetSettingByIdAsync")]
        [HasPermission(PermissionModules.Settings, PermissionAction.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetSettingByIdAsync(int SettingId)
        {
            if (SettingId < 1)
                return BadRequest("Invalid Data");

            var setting = await _settings.GetSettingByIdAsync(SettingId);

            if (setting == null)
                return NotFound("Setting not found");

            return Ok(setting);
        }

        [HttpPost("AddSetting", Name = "AddSettingAsync")]
        [HasPermission(PermissionModules.Settings, PermissionAction.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddSettingAsync([FromBody] CreateSettingDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.SettingName))
                return BadRequest("Invalid Data");

            var result = await _settings.AddSettingAsync(dto);

            return CreatedAtRoute("GetSettingByIdAsync",
                new { SettingId = result.SettingId }, $"Done added seting successfully with id: {result}");
        }

        [HttpPut("UpdateSetting", Name = "UpdateSettingAsync")]
        [HasPermission(PermissionModules.Settings, PermissionAction.Update)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateSettingAsync([FromBody] UpdatedSettingDTO updatedSetting)
        {
            if (updatedSetting == null ||updatedSetting.SettingId < 1 ||
                string.IsNullOrEmpty(updatedSetting.SettingName))
            {
                return BadRequest("Invalid Data");
            }

            if (!await _settings.IsSettingExistAsync(updatedSetting.SettingId))
                return NotFound("This setting is not found!");

            return await _settings.UpdateSettingAsync(updatedSetting) ? Ok("Setting updated successfully")
                : NotFound("Setting not found");
        }

        [HttpDelete("Delete/{SettingId}", Name = "DeleteSettingAsync")]
        [HasPermission(PermissionModules.Settings, PermissionAction.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteSettingAsync(int SettingId)
        {
            if (SettingId < 1)
                return BadRequest("Invalid Id");

            if (await _settings.DeleteSettingAsync(SettingId))
                return Ok("Done delete setting successfully.");
            else
                return NotFound("This setting is not found!");
        }

        [HttpGet("exist/{SettingId}", Name = "IsSettingExistByIdAsync")]
        [HasPermission(PermissionModules.Settings, PermissionAction.Read)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IsSettingExistByIdAsync(int SettingId)
        {
            bool isFound = await _settings.IsSettingExistAsync(SettingId);

            if (!isFound)
                return NotFound("Not Found");

            return Ok(isFound);
        }
    }
}
