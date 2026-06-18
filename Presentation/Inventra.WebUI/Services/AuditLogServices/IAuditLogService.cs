using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.AuditLogDtos;

namespace Inventra.WebUI.Services.AuditLogServices
{
    public interface IAuditLogService
    {
        Task<PagedResponse<ResultAuditLogDto>>
            GetAllAsync(
                AuditLogFilterDto filter);
    }
}
