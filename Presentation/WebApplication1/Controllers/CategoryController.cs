using Inventra.WebUI.Dtos.CategoryDtos;
using Inventra.WebUI.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(
            ICategoryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var categories =
                await _service.GetAllAsync();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _service.CreateAsync(model);

            return RedirectToAction(
                nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var category=await _service.GetByIdAsync(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }
            await _service.UpdateAsync(categoryDto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
