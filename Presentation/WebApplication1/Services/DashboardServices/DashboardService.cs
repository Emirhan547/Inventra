using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.DashboardDtos;

namespace Inventra.WebUI.Services.DashboardServices
{
    public class DashboardService: IDashboardService
    {
        private readonly HttpClient _client;

        public DashboardService(IHttpClientFactory httpClientFactory)
        {
            _client =httpClientFactory.CreateClient("InventraApi");
        }

        public async Task< DashboardSummaryDto?>GetDashboardAsync()
        {
            var response =await _client.GetFromJsonAsync<ApiResponse<DashboardSummaryDto>>("dashboard");
            return response?.Data;
        }
    }
}
