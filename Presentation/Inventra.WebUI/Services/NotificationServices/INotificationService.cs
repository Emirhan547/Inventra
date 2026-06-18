using Inventra.WebUI.Dtos.NotificationDtos;

namespace Inventra.WebUI.Services.NotificationServices
{
    public interface INotificationService
    {
        Task<List<ResultNotificationDto>> GetAllAsync();

        Task<int> GetUnreadCountAsync();

        Task MarkAllAsReadAsync();

        Task MarkAsReadAsync(Guid id);
    }
}
