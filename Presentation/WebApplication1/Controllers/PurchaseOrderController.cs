using Inventra.WebUI.Dtos.PurchaseOrders;
using Inventra.WebUI.Services.ProductServices;
using Inventra.WebUI.Services.PurchaseOrderServices;
using Inventra.WebUI.Services.SupplierServices;
using Inventra.WebUI.Services.WarehouseServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventra.WebUI.Controllers
{
    public class PurchaseOrderController(
     IPurchaseOrderService _purchaseOrderService,
     ISupplierService _supplierService,
     IProductService _productService,
     IWarehouseService _warehouseService)
     : Controller
    {
        public async Task<IActionResult>
            Index()
        {
            var orders =
                await _purchaseOrderService
                    .GetAllAsync();

            return View(orders);
        }

        public async Task<IActionResult>
            Create()
        {
            ViewBag.Suppliers =
                new SelectList(
                    await _supplierService
                        .GetAllAsync(),
                    "Id",
                    "Name");

            ViewBag.Products =
                new SelectList(
                    await _productService
                        .GetAllAsync(),
                    "Id",
                    "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult>
            Create(
                CreatePurchaseOrderDto model)
        {
            await _purchaseOrderService
                .CreateAsync(model);

            return RedirectToAction(
                nameof(Index));
        }

        public async Task<IActionResult>
            Details(Guid id)
        {
            var order =
                await _purchaseOrderService
                    .GetByIdAsync(id);

            return View(order);
        }

        public async Task<IActionResult>
            Approve(Guid id)
        {
            await _purchaseOrderService
                .ApproveAsync(id);

            return RedirectToAction(
                nameof(Index));
        }
        public async Task<IActionResult>
    Complete(Guid id)
        {
            var warehouses =
                await _warehouseService
                    .GetAllAsync();

            ViewBag.Warehouses =
                new SelectList(
                    warehouses,
                    "Id",
                    "Name");

            return View(
                new CompletePurchaseOrderDto
                {
                    PurchaseOrderId = id
                });
        }
        [HttpPost]
        public async Task<IActionResult>
    Complete(
        CompletePurchaseOrderDto model)
        {
            await _purchaseOrderService
                .CompleteAsync(model);

            return RedirectToAction(
                nameof(Index));
        }
    }
}
