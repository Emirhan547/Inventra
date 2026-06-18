using Inventra.WebUI.Services.NotificationServices;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.WebUI.Controllers
{
    public class NotificationController
    : Controller
    {
        private readonly INotificationService
            _notificationService;

        public NotificationController(
            INotificationService notificationService)
        {
            _notificationService =
                notificationService;
        }

        [HttpGet]
        public async Task<IActionResult>
            GetAll()
        {
            var notifications =
                await _notificationService
                    .GetAllAsync();

            return Json(notifications);
        }
        [HttpGet]
        public async Task<IActionResult>
    GetUnreadCount()
        {
            var count =
                await _notificationService
                    .GetUnreadCountAsync();

            return Json(count);
        }
        [HttpPut]
        public async Task<IActionResult>
    MarkAllAsRead()
        {
            await _notificationService
                .MarkAllAsReadAsync();

            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult>
    MarkAsRead(Guid id)
        {
            await _notificationService
                .MarkAsReadAsync(id);

            return Ok();
        }
    }
}