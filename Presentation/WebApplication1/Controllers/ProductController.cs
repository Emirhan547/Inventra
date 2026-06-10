using Inventra.WebUI.Dtos.ProductDtos;
using Inventra.WebUI.Services.CategoryServices;
using Inventra.WebUI.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventra.WebUI.Controllers
{
    public class ProductController(IProductService _productService,ICategoryService _categoryService): Controller
    {
        public async Task<IActionResult>Index()
        {
            var products =await _productService.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult>Create()
        {
            var categories =await _categoryService .GetAllAsync();
            ViewBag.Categories =new SelectList(categories,"Id","Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(CreateProductDto model)
        {
            await _productService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult>Update(Guid id)
        {
            var product =await _productService.GetByIdAsync(id);
            if (product is null)
            {
                return RedirectToAction(nameof(Index));
            }
            var categories =await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories,"Id","Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductDto model)
        {
            await _productService.UpdateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult>Delete(Guid id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
