using ERP.Contacts.Requests.People;
using ERP.Contacts.Requests.Settings;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Mappings
{
    public static class SettingsMapping
    {
           public static Setting ToEntity(this CreateSettingDTO dto)
        {
            return new Setting(
                0,
                dto.SettingName,
                dto.SettingValue
               );
        }


        public static Setting ToEntity(this UpdatedSettingDTO dto)
        {
           return new Setting(
                dto.SettingId,
                dto.SettingName,
                dto.SettingValue
               );
        }


        public static SettingResponseDTO ToResponseDTO(this Setting setting)
        {
            return new SettingResponseDTO
            {
               SettingId=setting.SettingId,
               SettingName=setting.SettingName,
               SettingValue=setting.SettingValue
            };
        }



    }
}
