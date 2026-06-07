using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.ProductDtos;

namespace Inventra.WebUI.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;

        public ProductService(
            IHttpClientFactory httpClientFactory)
        {
            _client =
                httpClientFactory
                    .CreateClient("InventraApi");
        }

        public async Task<List<ResultProductDto>>
            GetAllAsync()
        {
            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<List<ResultProductDto>>>(
                        "products");

            return response?.Data ?? [];
        }

        public async Task<UpdateProductDto?>
            GetByIdAsync(Guid id)
        {
            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<UpdateProductDto>>(
                        $"products/{id}");

            return response?.Data;
        }

        public async Task CreateAsync(
            CreateProductDto model)
        {
            await _client.PostAsJsonAsync(
                "products",
                model);

        }

        public async Task UpdateAsync(
            UpdateProductDto model)
        {
            await _client.PutAsJsonAsync(
                $"products/{model.Id}",
                model);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _client.DeleteAsync(
                $"products/{id}");
        }
    }
}
