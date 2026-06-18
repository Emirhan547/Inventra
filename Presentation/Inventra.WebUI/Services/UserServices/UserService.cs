using System.Net.Http.Json;
using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.UserDtos;

namespace Inventra.WebUI.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;

        public UserService(
            IHttpClientFactory httpClientFactory)
        {
            _client =
                httpClientFactory
                    .CreateClient("InventraApi");
        }

        public async Task<PagedResponse<ResultUserDto>>
            GetAllAsync(UserFilterDto filter)
        {
            var url =
                $"users?pageNumber={filter.PageNumber}" +
                $"&pageSize={filter.PageSize}";

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                url += $"&search={filter.Search}";
            }

            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<PagedResponse<ResultUserDto>>>(url);

            return response?.Data
                   ?? new PagedResponse<ResultUserDto>();
        }

        public async Task<UserDetailDto?>
            GetByIdAsync(Guid id)
        {
            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<UserDetailDto>>
                    ($"users/{id}");

            return response?.Data;
        }

        public async Task<UserRolesDto?>
            GetRolesAsync(Guid id)
        {
            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<UserRolesDto>>
                    ($"users/{id}/roles");

            return response?.Data;
        }

        public async Task AssignRoleAsync(
            Guid userId,
            string roleName)
        {
            await _client.PostAsJsonAsync(
                $"users/{userId}/roles",
                new
                {
                    RoleName = roleName
                });
        }

        public async Task RemoveRoleAsync(
            Guid userId,
            string roleName)
        {
            await _client.DeleteAsync(
                $"users/{userId}/roles/{roleName}");
        }
    }
}