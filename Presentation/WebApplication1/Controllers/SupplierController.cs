using Inventra.WebUI.Dtos.SupplierDtos;
using Inventra.WebUI.Services.SupplierServices;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.WebUI.Controllers
{
    public class SupplierController( ISupplierService _supplierService): Controller
    {
        public async Task<IActionResult>Index()
        {
            var suppliers = await _supplierService .GetAllAsync();
            return View(suppliers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(CreateSupplierDto model)
        {
            await _supplierService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult>Update(Guid id)
        {
            var supplier = await _supplierService .GetByIdAsync(id);

            if (supplier is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult>Update( UpdateSupplierDto model)
        {
            await _supplierService .UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult>Delete(Guid id)
        {
            await _supplierService.DeleteAsync(id);
            return RedirectToAction( nameof(Index));
        }
    }
}
