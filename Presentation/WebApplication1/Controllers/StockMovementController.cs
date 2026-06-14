using Inventra.WebUI.Constants;
using Inventra.WebUI.Services.StockMovementServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.WebUI.Controllers
{
    [Authorize(Roles = RoleGroups.AllUsers)]
    public class StockMovementController(IStockMovementService _service): Controller
    {
        public async Task<IActionResult> Index()
        {
            var movements = await _service.GetAllAsync();
            return View(movements);
        }
    }
}
