using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.PurchaseOrders;

namespace Inventra.WebUI.Services.PurchaseOrderServices
{
    public class PurchaseOrderService: IPurchaseOrderService
    {
        private readonly HttpClient _client;

        public PurchaseOrderService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("InventraApi");
        }

        public async Task<
    PagedResponse<ResultPurchaseOrderDto>>
    GetAllAsync(
        PurchaseOrderFilterDto filter)
        {
            var query =
                $"purchase-orders?" +
                $"PageNumber={filter.PageNumber}" +
                $"&PageSize={filter.PageSize}";

            if (filter.Status.HasValue)
            {
                query +=
                    $"&Status={(int)filter.Status.Value}";
            }

            if (filter.SupplierId.HasValue)
            {
                query +=
                    $"&SupplierId={filter.SupplierId}";
            }

            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<
                        PagedResponse<
                            ResultPurchaseOrderDto>>>
                    (query);

            return response?.Data
                   ?? new PagedResponse<
                       ResultPurchaseOrderDto>();
        }

        public async Task CreateAsync(CreatePurchaseOrderDto model)
        {
            var request = new
                {
                    SupplierId =model.SupplierId,

                    Items =new[]
                        {
                            new
                            {
                                ProductId =model.ProductId,
                                Quantity =model.Quantity,
                                UnitPrice =model.UnitPrice
                            }
                        }
                };

            await _client.PostAsJsonAsync("purchase-orders",request);
        }
        public async Task<PurchaseOrderDetailDto?> GetByIdAsync(Guid id)
        {
            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<PurchaseOrderDetailDto>>(
                        $"purchase-orders/{id}");

            return response?.Data;
        }

        public async Task ApproveAsync(Guid id)
        {
            await _client.PatchAsync($"purchase-orders/{id}/approve",null);
        }

        public async Task CompleteAsync(CompletePurchaseOrderDto model)
        {
            await _client.PatchAsync($"purchase-orders/{model.PurchaseOrderId}/complete?warehouseId={model.WarehouseId}",null);
        }
        public async Task<PurchaseOrderAiAnalysisDto?> GetAiAnalysisAsync(Guid id)
        {
            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<PurchaseOrderAiAnalysisDto>>
                    ($"purchase-orders/{id}/ai-analysis");

            return response?.Data;
        }
    }
}
