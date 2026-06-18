using Inventra.WebUI.Dtos.HealthDtos;

namespace Inventra.WebUI.Services.HealthServices
{
    public sealed class HealthService
        : IHealthService
    {
        private readonly HttpClient _client;

        public HealthService(
            IHttpClientFactory factory)
        {
            _client =
                factory.CreateClient(
                    "InventraApi");
        }

        public async Task<HealthResponseDto>
            GetStatusAsync()
        {
            return await _client
                .GetFromJsonAsync<
                    HealthResponseDto>(
                    "health")
                ?? new HealthResponseDto();
        }
    }
}