namespace ERP.Domain.Entities
{
    public class Setting
    {
        public int SettingId { get; set; }
        public string SettingName { get; set; }
        public string? SettingValue { get; set; }

        public Setting(int settingId, string settingName, string? settingValue)
        {
            this.SettingId = settingId;
            this.SettingName = settingName;
            this.SettingValue = settingValue;
        }
    }
}
