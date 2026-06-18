using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.ProductDtos;

namespace Inventra.WebUI.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;

        public ProductService( IHttpClientFactory httpClientFactory)
        {
            _client =httpClientFactory.CreateClient("InventraApi");
        }

        public async Task<PagedResponse<ResultProductDto>>GetAllAsync(ProductFilterDto? filter = null)
        {
            filter ??= new ProductFilterDto();
            var url =$"products?pageNumber={filter.PageNumber}" +$"&pageSize={filter.PageSize}";
            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                url += $"&search={filter.Search}";
            }
            if (filter.CategoryId.HasValue)
            {
                url += $"&categoryId={filter.CategoryId}";
            }
            var response =await _client.GetFromJsonAsync<ApiResponse<PagedResponse<ResultProductDto>>>(url);
            return response?.Data?? new PagedResponse<ResultProductDto>();
        }

        public async Task<UpdateProductDto?> GetByIdAsync(Guid id)
        {
            var response =await _client.GetFromJsonAsync<ApiResponse<UpdateProductDto>>( $"products/{id}");
            return response?.Data;
        }

        public async Task CreateAsync(CreateProductDto model)
        {
            await _client.PostAsJsonAsync("products",model);

        }

        public async Task UpdateAsync(UpdateProductDto model)
        {
            await _client.PutAsJsonAsync( $"products/{model.Id}",model);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _client.DeleteAsync($"products/{id}");
        }
    }
}
