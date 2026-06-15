using Inventra.WebUI.Dtos.NotificationDtos;

namespace Inventra.WebUI.Services.NotificationServices
{
    public sealed class NotificationService
     : INotificationService
    {
        private readonly HttpClient _client;

        public NotificationService(
            IHttpClientFactory httpClientFactory)
        {
            _client =
                httpClientFactory
                    .CreateClient("InventraApi");
        }

        public async Task<List<ResultNotificationDto>>
            GetAllAsync()
        {
            return await _client
                .GetFromJsonAsync<
                    List<ResultNotificationDto>>
                        ("notifications")
                   ?? [];
        }
    }
}