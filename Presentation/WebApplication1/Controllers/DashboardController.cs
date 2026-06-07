using Inventra.WebUI.Services.DashboardServices;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.WebUI.Controllers
{
    public class DashboardController(
          IDashboardService _dashboardService)
          : Controller
    {
        public async Task<IActionResult>
            Index()
        {
            var data =
                await _dashboardService
                    .GetDashboardAsync();

            return View(data);
        }
    }
}
