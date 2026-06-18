using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.AuditLogDtos;

namespace Inventra.WebUI.Services.AuditLogServices
{
    public sealed class AuditLogService
     : IAuditLogService
    {
        private readonly HttpClient _client;

        public AuditLogService(
            IHttpClientFactory httpClientFactory)
        {
            _client =
                httpClientFactory
                    .CreateClient("InventraApi");
        }

        public async Task<PagedResponse<ResultAuditLogDto>>
     GetAllAsync(AuditLogFilterDto filter)
        {
            var query =
                $"auditlogs?" +
                $"PageNumber={filter.PageNumber}" +
                $"&PageSize={filter.PageSize}";

            if (!string.IsNullOrWhiteSpace(filter.EventName))
            {
                query += $"&EventName={filter.EventName}";
            }

            if (!string.IsNullOrWhiteSpace(filter.UserName))
            {
                query += $"&UserName={filter.UserName}";
            }

            if (filter.StartDate.HasValue)
            {
                query += $"&StartDate={filter.StartDate:yyyy-MM-dd}";
            }

            if (filter.EndDate.HasValue)
            {
                query += $"&EndDate={filter.EndDate:yyyy-MM-dd}";
            }

            var response =
                await _client.GetFromJsonAsync<
                    PagedResponse<ResultAuditLogDto>>
                (query);

            return response
                   ?? new PagedResponse<ResultAuditLogDto>();
        }
    }
}