using Inventra.WebUI.Services.StockMovementServices;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.WebUI.Controllers
{
    public class StockMovementController(
        IStockMovementService _service)
        : Controller
    {
        public async Task<IActionResult>
            Index()
        {
            var movements =
                await _service
                    .GetAllAsync();

            return View(movements);
        }
    }
}
