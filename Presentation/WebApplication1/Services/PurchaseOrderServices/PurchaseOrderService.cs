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

        public async Task<List<ResultPurchaseOrderDto>> GetAllAsync()
        {
            var response =await _client.GetFromJsonAsync<ApiResponse< List<ResultPurchaseOrderDto>>>("puchaseOrder");

            return response?.Data ?? [];
        }

        public async Task<PurchaseOrderDetailDto?>GetByIdAsync(Guid id)
        {
            var response =await _client.GetFromJsonAsync< ApiResponse<PurchaseOrderDetailDto>>($"puchaseOrder/{id}");
            return response?.Data;
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

            await _client.PostAsJsonAsync("puchaseOrder",request);
        }

        public async Task ApproveAsync(Guid id)
        {
            await _client.PatchAsync($"puchaseOrder/{id}/approve",null);
        }

        public async Task CompleteAsync(CompletePurchaseOrderDto model)
        {
            await _client.PatchAsync($"puchaseOrder/{model.PurchaseOrderId}/complete?warehouseId={model.WarehouseId}",null);
        }
    }
}
