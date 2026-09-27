using ERP.Domain.Entities;

namespace ERP.Application.Interfaces.Repositories;

public interface IAuditLogRepository
{
    Task<List<AuditLog>> GetAllAsync();
    Task<List<AuditLog>> GetByUserIdAsync(int userId);
    Task<int> AddAsync(AuditLog auditLog);
}
