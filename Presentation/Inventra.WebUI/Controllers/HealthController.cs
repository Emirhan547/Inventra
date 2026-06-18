using Inventra.WebUI.Constants;
using Inventra.WebUI.Services.HealthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.WebUI.Controllers
{
    [Authorize(Roles = RoleGroups.AdminOnly)]
    public sealed class HealthController
        : Controller
    {
        private readonly IHealthService
            _healthService;

        public HealthController(
            IHealthService healthService)
        {
            _healthService =
                healthService;
        }

        public async Task<IActionResult>
            Index()
        {
            var result =
                await _healthService
                    .GetStatusAsync();

            return View(result);
        }
    }
}