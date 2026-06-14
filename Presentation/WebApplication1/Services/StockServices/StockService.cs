using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.StockDtos;

namespace Inventra.WebUI.Services.StockServices
{
    public class StockService : IStockService
    {
        private readonly HttpClient _client;

        public StockService(
            IHttpClientFactory httpClientFactory)
        {
            _client =
                httpClientFactory
                    .CreateClient("InventraApi");
        }
        public async Task<PagedResponse<ResultStockDto>>
     GetAllAsync()
        {
            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<
                        PagedResponse<ResultStockDto>>>(
                            "stocks");

            return response?.Data
                   ?? new PagedResponse<ResultStockDto>();
        }
        public async Task StockInAsync(
    CreateStockInDto model)
        {
            await _client.PostAsJsonAsync(
                "stocks/in",
                model);
        }
        public async Task StockOutAsync(
    CreateStockOutDto model)
        {
            await _client.PostAsJsonAsync(
                "stocks/out",
                model);
        }
        public async Task TransferAsync(
    CreateTransferStockDto model)
        {
            await _client.PostAsJsonAsync(
                "stocks/transfer",
                model);
        }
    }
}
