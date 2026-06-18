using Inventra.WebUI.Constants;
using Inventra.WebUI.Dtos.UserDtos;
using Inventra.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.WebUI.Controllers
{
    [Authorize(Roles = RoleGroups.AdminOnly)]
    public class UserController(IUserService _userService): Controller
    {
        public async Task<IActionResult>
            Index(UserFilterDto filter)
        {
            var users = await _userService.GetAllAsync(filter);
            return View(users);
        }

        public async Task<IActionResult>Details(Guid id)
        {
            var user =await _userService.GetByIdAsync(id);

            if (user is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        public async Task<IActionResult>
            Roles(Guid id)
        {
            var roles =
                await _userService
                    .GetRolesAsync(id);

            ViewBag.UserId = id;

            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult>
            AssignRole(
                Guid userId,
                string roleName)
        {
            await _userService
                .AssignRoleAsync(
                    userId,
                    roleName);

            return RedirectToAction(
                nameof(Roles),
                new { id = userId });
        }

        [HttpPost]
        public async Task<IActionResult>
            RemoveRole(
                Guid userId,
                string roleName)
        {
            await _userService
                .RemoveRoleAsync(
                    userId,
                    roleName);

            return RedirectToAction(
                nameof(Roles),
                new { id = userId });
        }
    }
}