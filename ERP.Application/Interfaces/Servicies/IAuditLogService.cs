using ERP.Contacts.Requests.AuditLogs;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Services;

public interface IAuditLogService
{
    Task<List<AuditLogResponseDTO>> GetAllAsync();
    Task<List<AuditLogResponseDTO>> GetByUserIdAsync(int userId);
    Task<int> AddAsync(int userId, CreateAuditLogDTO logDTO);
}
