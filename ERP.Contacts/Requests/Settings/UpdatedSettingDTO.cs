namespace ERP.Contacts.Requests.Settings
{
    public sealed record UpdatedSettingDTO
    {
        public int SettingId { get; set; }
        public string SettingName { get; set; }
        public string? SettingValue { get; set; }
    }
}
