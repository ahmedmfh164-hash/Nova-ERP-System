namespace ERP.Contacts.Responses
{
    public sealed record SettingResponseDTO
    {
        public int SettingId { get; set; }
        public string SettingName { get; set; }
        public string? SettingValue { get; set; }
    }
}
