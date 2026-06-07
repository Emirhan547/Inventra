using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.CategoryDtos;

namespace Inventra.WebUI.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _client;

        public CategoryService(
            IHttpClientFactory httpClientFactory)
        {
            _client =
                httpClientFactory
                    .CreateClient("InventraApi");
        }

        public async Task<List<ResultCategoryDto>>
            GetAllAsync()
        {
            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<List<ResultCategoryDto>>>(
                        "categories");

            return response?.Data ?? [];
        }

        public async Task<UpdateCategoryDto?>
            GetByIdAsync(Guid id)
        {
            var response =
                await _client.GetFromJsonAsync<ApiResponse<UpdateCategoryDto>>(
                        $"categories/{id}");

            return response?.Data;
        }

        public async Task CreateAsync(
            CreateCategoryDto model)
        {
            await _client.PostAsJsonAsync(
                "categories",
                model);
        }

        public async Task UpdateAsync(
            UpdateCategoryDto model)
        {
            await _client.PutAsJsonAsync(
                $"categories/{model.Id}",
                model);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _client.DeleteAsync(
                $"categories/{id}");
        }
    }
}