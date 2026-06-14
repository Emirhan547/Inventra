using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.StockMovementDtos;

namespace Inventra.WebUI.Services.StockMovementServices
{
    public class StockMovementService: IStockMovementService
    {
        private readonly HttpClient _client;

        public StockMovementService(IHttpClientFactory httpClientFactory)
        {
            _client =httpClientFactory.CreateClient("InventraApi");
        }

        public async Task<PagedResponse<ResultStockMovementDto>>
     GetAllAsync()
        {
            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<
                        PagedResponse<ResultStockMovementDto>>>(
                            "stock-movements");

            return response?.Data
                   ?? new PagedResponse<ResultStockMovementDto>();
        }
    }
}
