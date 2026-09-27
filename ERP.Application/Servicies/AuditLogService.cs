using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services;
using ERP.Contacts.Mappings;
using ERP.Contacts.Requests.AuditLogs;
using ERP.Contacts.Responses;
using ERP.Domain.Entities;

namespace ERP.Application.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IAuditLogRepository _repository;

    public AuditLogService(IAuditLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<AuditLogResponseDTO>> GetAllAsync()
    {
        var logs = await _repository.GetAllAsync();

        return logs.Select(x => x.ToResponseDTO()).ToList();
    }

    public async Task<List<AuditLogResponseDTO>> GetByUserIdAsync(int userId)
    {
        var logs = await _repository.GetByUserIdAsync(userId);

        return logs.Select(x => x.ToResponseDTO()).ToList();
    }

    public async Task<int> AddAsync(int userId,CreateAuditLogDTO logDTO)
    {
        var log = logDTO.ToEntity();
        log.UserId=userId;
        return await _repository.AddAsync(log);
    }
}
