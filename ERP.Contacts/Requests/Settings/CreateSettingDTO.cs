namespace ERP.Contacts.Requests.Settings
{
    public sealed record CreateSettingDTO
    {
        public string SettingName { get; set; }
        public string? SettingValue { get; set; }
    }
}
