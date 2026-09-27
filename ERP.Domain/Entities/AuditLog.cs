namespace ERP.Domain.Entities;

public class AuditLog
{
    public int LogId { get; set; }
    public int UserId { get; set; }
    public string Action { get; set; }
    public DateTime LogDate { get; set; }

    public AuditLog(int logId,int userId,string action,DateTime logDate)
    {
        LogId=logId;
        UserId=userId;
        Action=action;
        LogDate=logDate;
    }

}
