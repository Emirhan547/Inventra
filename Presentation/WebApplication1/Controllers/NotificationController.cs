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
    }
}