using Inventra.WebUI.Dtos.StockDtos;
using Inventra.WebUI.Services.ProductServices;
using Inventra.WebUI.Services.StockServices;
using Inventra.WebUI.Services.WarehouseServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventra.WebUI.Controllers
{
    public class StockController(IStockService _stockService, IProductService _productService, IWarehouseService _warehouseService): Controller
    {
        public async Task<IActionResult> Index()
        {
            var stocks =await _stockService.GetAllAsync();
            return View(stocks);
        }
        public async Task<IActionResult>StockIn()
        {
            var products = await _productService.GetAllAsync();
            var warehouses = await _warehouseService.GetAllAsync();

            ViewBag.Products =new SelectList(products,"Id","Name");
            ViewBag.Warehouses =new SelectList( warehouses, "Id","Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult>StockIn(CreateStockInDto model)
        {
            await _stockService.StockInAsync(model);
            return RedirectToAction( nameof(Index));
        }
        public async Task<IActionResult>StockOut()
        {
            var products = await _productService.GetAllAsync();
            var warehouses = await _warehouseService.GetAllAsync();
            ViewBag.Products = new SelectList( products, "Id","Name");
            ViewBag.Warehouses = new SelectList( warehouses, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StockOut(CreateStockOutDto model)
        {
            await _stockService.StockOutAsync(model);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult>Transfer()
        {
            var products = await _productService .GetAllAsync();
            var warehouses = await _warehouseService.GetAllAsync();
            ViewBag.Products =new SelectList( products,"Id","Name");
            ViewBag.Warehouses =new SelectList( warehouses,"Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Transfer(CreateTransferStockDto model)
        {
            await _stockService.TransferAsync(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
