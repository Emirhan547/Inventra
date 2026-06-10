using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.WarehouseDtos;

namespace Inventra.WebUI.Services.WarehouseServices
{
    public class WarehouseService : IWarehouseService
    {
        private readonly HttpClient _client;

        public WarehouseService(IHttpClientFactory httpClientFactory)
        {
            _client =httpClientFactory.CreateClient("InventraApi");
        }

        public async Task<List<ResultWarehouseDto>>GetAllAsync()
        {
            var response =await _client.GetFromJsonAsync<ApiResponse<List<ResultWarehouseDto>>>("warehouses");

            return response?.Data ?? [];
        }

        public async Task<UpdateWarehouseDto?>GetByIdAsync(Guid id)
        {
            var response =await _client.GetFromJsonAsync<ApiResponse<UpdateWarehouseDto>>($"warehouses/{id}");
            return response?.Data;
        }

        public async Task CreateAsync(CreateWarehouseDto model)
        {
            await _client.PostAsJsonAsync("warehouses",model);
        }

        public async Task UpdateAsync(UpdateWarehouseDto model)
        {
            await _client.PutAsJsonAsync($"warehouses/{model.Id}",model);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _client.DeleteAsync($"warehouses/{id}");
        }
    }
}
