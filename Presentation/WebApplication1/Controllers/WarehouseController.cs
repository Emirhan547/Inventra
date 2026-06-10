using Inventra.WebUI.Dtos.WarehouseDtos;
using Inventra.WebUI.Services.WarehouseServices;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.WebUI.Controllers
{
    public class WarehouseController(IWarehouseService _warehouseService): Controller
    {
        public async Task<IActionResult> Index()
        {
            var warehouses =await _warehouseService.GetAllAsync();
            return View(warehouses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(CreateWarehouseDto model)
        {
            await _warehouseService.CreateAsync(model);
            return RedirectToAction( nameof(Index));
        }

        public async Task<IActionResult>Update(Guid id)
        {
            var warehouse = await _warehouseService.GetByIdAsync(id);

            if (warehouse is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(warehouse);
        }

        [HttpPost]
        public async Task<IActionResult>Update(UpdateWarehouseDto model)
        {
            await _warehouseService.UpdateAsync(model);
            return RedirectToAction(  nameof(Index));
        }

        public async Task<IActionResult>Delete(Guid id)
        {
            await _warehouseService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
