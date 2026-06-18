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

        public async Task<
            PagedResponse<ResultStockMovementDto>>
            GetAllAsync(
                StockMovementFilterDto filter)
        {
            var query =
                $"stock-movements?" +
                $"PageNumber={filter.PageNumber}" +
                $"&PageSize={filter.PageSize}";

            if (filter.Type.HasValue)
            {
                query +=
                    $"&Type={(int)filter.Type.Value}";
            }

            if (filter.StartDate.HasValue)
            {
                query +=
                    $"&StartDate={filter.StartDate.Value:yyyy-MM-dd}";
            }

            if (filter.EndDate.HasValue)
            {
                query +=
                    $"&EndDate={filter.EndDate.Value:yyyy-MM-dd}";
            }

            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<
                        PagedResponse<
                            ResultStockMovementDto>>>
                    (query);

            return response?.Data
                ?? new PagedResponse<
                    ResultStockMovementDto>();
        }
    }
}
