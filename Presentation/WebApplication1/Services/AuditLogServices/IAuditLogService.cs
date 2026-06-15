using Inventra.WebUI.Dtos.AuditLogDtos;

namespace Inventra.WebUI.Services.AuditLogServices
{
    public interface IAuditLogService
    {
        Task<List<ResultAuditLogDto>>
            GetAllAsync();
    }
}
