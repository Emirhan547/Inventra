using Inventra.WebUI.Constants;
using Inventra.WebUI.Dtos.ProfileDtos;
using Inventra.WebUI.Services.ProfileServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.WebUI.Controllers;

[Authorize(Roles = RoleGroups.AllUsers)]
public class ProfileController
    : Controller
{
    private readonly IProfileService
        _profileService;

    public ProfileController(
        IProfileService profileService)
    {
        _profileService =
            profileService;
    }

    public async Task<IActionResult>
        Index()
    {
        var model =
            await _profileService.GetAsync();

        return View(model);
    }
    public async Task<IActionResult> Edit()
    {
        var profile =
            await _profileService.GetAsync();

        var model =
            new UpdateProfileDto
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                UserName = profile.UserName,
                Email = profile.Email
            };

        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> Edit(
     UpdateProfileDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _profileService.UpdateAsync(model);

        return RedirectToAction(nameof(Index));
    }
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(
    ChangePasswordDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _profileService
            .ChangePasswordAsync(model);

        return RedirectToAction(nameof(Index));
    }

}