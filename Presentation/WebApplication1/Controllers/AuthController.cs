using Inventra.WebUI.Dtos.LoginDtos;
using Inventra.WebUI.Dtos.RegisterDtos;
using Inventra.WebUI.Services.AuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.WebUI.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result =await _authService.LoginAsync(model, HttpContext);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty,result.Message);

                return View(model);
            }
            return RedirectToAction("Index","Dashboard");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _authService.RegisterAsync(model);

            if (!result.Success)
            {
                if (result.Errors is not null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty,error);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty,result.Message);
                }

                return View(model);
            }

            TempData["Success"] ="Kayıt işlemi başarılı. Giriş yapabilirsiniz.";

            return RedirectToAction(nameof(Login));
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync(HttpContext);

            return RedirectToAction(nameof(Login));
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}