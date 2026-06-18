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
        public async Task<string> UpdateAsync(
    UpdateProfileDto model)
        {
            var result =
                await _client.PutAsJsonAsync(
                "profile",
                model);

            return await ReadResultAsync(result);
        }
        public async Task<string> ChangePasswordAsync(
    ChangePasswordDto model)
        {
            var result =
                await _client.PutAsJsonAsync(
                "profile/change-password",
                model);

            return await ReadResultAsync(result);
        }

        private static async Task<string> ReadResultAsync(
            HttpResponseMessage response)
        {
            var apiResponse =
                await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

            if (!response.IsSuccessStatusCode ||
                apiResponse is null ||
                !apiResponse.Success)
            {
                var errors =
                    apiResponse?.Errors?
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToList()
                    ?? [];

                if (!string.IsNullOrWhiteSpace(apiResponse?.Message))
                {
                    errors.Insert(
                        0,
                        apiResponse.Message);
                }

                throw new InvalidOperationException(
                    errors.Count > 0
                        ? string.Join(Environment.NewLine, errors)
                        : "İşlem tamamlanamadı.");
            }

            return apiResponse.Message;
        }

    }
}
