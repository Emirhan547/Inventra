using Inventra.WebUI.Dtos.NotificationDtos;

namespace Inventra.WebUI.Services.NotificationServices
{
    public interface INotificationService
    {
        Task<List<ResultNotificationDto>> GetAllAsync();
    }
}
