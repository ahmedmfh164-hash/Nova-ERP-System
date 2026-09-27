namespace ERP.Contacts.Requests.AuditLogs;

public sealed record CreateAuditLogDTO
{
    public string Action { get; set; }
}
