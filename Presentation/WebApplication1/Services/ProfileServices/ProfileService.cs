using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.ProfileDtos;

namespace Inventra.WebUI.Services.ProfileServices
{
    public class ProfileService
    : IProfileService
    {
        private readonly HttpClient _client;

        public ProfileService(
            IHttpClientFactory factory)
        {
            _client =
                factory.CreateClient("InventraApi");
        }

        public async Task<ResultProfileDto>
            GetAsync()
        {
            var response =
                await _client.GetFromJsonAsync<
                    ApiResponse<ResultProfileDto>>(
                    "profile");

            return response?.Data
                ?? new ResultProfileDto();
        }
        public async Task UpdateAsync(
    UpdateProfileDto model)
        {
            await _client.PutAsJsonAsync(
                "profile",
                model);
        }
        public async Task ChangePasswordAsync(
    ChangePasswordDto model)
        {
            await _client.PutAsJsonAsync(
                "profile/change-password",
                model);
        }

    }
}