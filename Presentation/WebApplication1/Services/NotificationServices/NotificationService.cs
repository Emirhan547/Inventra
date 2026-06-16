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
        public async Task<int> GetUnreadCountAsync()
        {
            return await _client
                .GetFromJsonAsync<int>(
                    "notifications/unread-count");
        }

        public async Task MarkAllAsReadAsync()
        {
            await _client.PutAsync(
                "notifications/read-all",
                null);
        }

        public async Task MarkAsReadAsync(Guid id)
        {
            await _client.PutAsync(
                $"notifications/{id}/read",
                null);
        }
    }
}