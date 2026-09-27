using ERP.Contacts.Requests.AuditLogs;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Contacts.Mappings
{
    public static class AuditLogMapping
    {
              public static AuditLog ToEntity(this CreateAuditLogDTO dto)
        {
            return new AuditLog(
                0,
                0,
                dto.Action,
                DateTime.Now
                );
        }

   
        public static AuditLogResponseDTO ToResponseDTO(this AuditLog log)
        {
            return new AuditLogResponseDTO
            {
              LogId=log.LogId,
              UserId=log.UserId,
              LogDate=log.LogDate,
              Action=log.Action
            };
        }


    }
}
