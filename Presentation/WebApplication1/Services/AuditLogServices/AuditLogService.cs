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

        public async Task<List<ResultAuditLogDto>>
            GetAllAsync()
        {
            return await _client
                .GetFromJsonAsync<
                    List<ResultAuditLogDto>>(
                    "auditlogs")
                ?? [];
        }
    }
}