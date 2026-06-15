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
        public async Task<
    PagedResponse<ResultStockDto>>
    GetAllAsync(
        StockFilterDto filter)
        {
            var query =
                $"stocks?" +
                $"PageNumber={filter.PageNumber}" +
                $"&PageSize={filter.PageSize}";

            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<
                        PagedResponse<
                            ResultStockDto>>>
                    (query);

            return response?.Data
                   ?? new PagedResponse<
                       ResultStockDto>();
        }
        public async Task StockInAsync(CreateStockInDto model)
        {
            var response = await _client.PostAsJsonAsync(
                "stocks/in",
                model);

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"STATUS: {response.StatusCode}");
            Console.WriteLine(content);

            response.EnsureSuccessStatusCode();
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
