using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.SupplierDtos;

namespace Inventra.WebUI.Services.SupplierServices
{
    public class SupplierService : ISupplierService
    {
        private readonly HttpClient _client;

        public SupplierService(IHttpClientFactory httpClientFactory)
        {
            _client =httpClientFactory.CreateClient("InventraApi");
        }

        public async Task<List<ResultSupplierDto>>GetAllAsync()
        {
            var response =await _client.GetFromJsonAsync<ApiResponse<List<ResultSupplierDto>>>( "suppliers");
            return response?.Data ?? [];
        }

        public async Task<UpdateSupplierDto?>GetByIdAsync(Guid id)
        {
            var response =await _client.GetFromJsonAsync<ApiResponse<UpdateSupplierDto>>($"suppliers/{id}");
            return response?.Data;
        }

        public async Task CreateAsync(CreateSupplierDto model)
        {
            await _client.PostAsJsonAsync("suppliers",model);
        }

        public async Task UpdateAsync(UpdateSupplierDto model)
        {
            await _client.PutAsJsonAsync($"suppliers/{model.Id}", model);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _client.DeleteAsync($"suppliers/{id}");
        }
    }
}
