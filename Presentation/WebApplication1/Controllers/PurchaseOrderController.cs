using Inventra.WebUI.Constants;
using Inventra.WebUI.Dtos.PurchaseOrders;
using Inventra.WebUI.Services.ProductServices;
using Inventra.WebUI.Services.PurchaseOrderServices;
using Inventra.WebUI.Services.SupplierServices;
using Inventra.WebUI.Services.WarehouseServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventra.WebUI.Controllers
{
    [Authorize(Roles = RoleGroups.AdminAndManager)]
    public class PurchaseOrderController(IPurchaseOrderService _purchaseOrderService,ISupplierService _supplierService,IProductService _productService, IWarehouseService _warehouseService): Controller
    {
        public async Task<IActionResult>
    Index(
        PurchaseOrderFilterDto filter)
        {
            var orders =
                await _purchaseOrderService
                    .GetAllAsync(filter);

            var suppliers =
                await _supplierService
                    .GetAllAsync();

            ViewBag.Suppliers =
                new SelectList(
                    suppliers,
                    "Id",
                    "Name",
                    filter.SupplierId);

            return View(orders);
        }

        public async Task<IActionResult>Create()
        {
            ViewBag.Suppliers =new SelectList(await _supplierService.GetAllAsync(),"Id","Name");
            var products =await _productService.GetAllAsync();

            ViewBag.Products =new SelectList(products.Items,"Id","Name"); 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(CreatePurchaseOrderDto model)
        {
            await _purchaseOrderService.CreateAsync(model);
            return RedirectToAction( nameof(Index));
        }

        public async Task<IActionResult>Details(Guid id)
        {
            var order = await _purchaseOrderService .GetByIdAsync(id);
            return View(order);
        }

        public async Task<IActionResult> Approve(Guid id)
        {
            await _purchaseOrderService.ApproveAsync(id);
            return RedirectToAction( nameof(Index));
        }
        public async Task<IActionResult>Complete(Guid id)
        {
            var warehouses =await _warehouseService.GetAllAsync();
            ViewBag.Warehouses = new SelectList( warehouses,"Id","Name");

            return View( new CompletePurchaseOrderDto
                {
                    PurchaseOrderId = id
                });
        }
        [HttpPost]
        public async Task<IActionResult> Complete(
        CompletePurchaseOrderDto model)
        {
            await _purchaseOrderService .CompleteAsync(model);
            return RedirectToAction( nameof(Index));
        }
    }
}
