using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Profiles.Commands;
using Inventra.Application.Features.Profiles.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Identity
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly ICurrentUserService _currentUserService;

        public ProfileService(
            UserManager<AppUser> userManager,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<Result<GetProfileQueryResponse>>
            GetProfileAsync()
        {
            var user =
                await _userManager.FindByIdAsync(
                    _currentUserService.UserId.ToString());

            if (user is null)
            {
                return Result<GetProfileQueryResponse>
                    .Failure("Kullanıcı bulunamadı.");
            }

            var roles =
                await _userManager.GetRolesAsync(user);

            var response =
                new GetProfileQueryResponse
                {
                    Id = user.Id,

                    FirstName = user.FirstName,

                    LastName = user.LastName,

                    UserName = user.UserName!,

                    Email = user.Email!,

                    Roles = roles.ToList()
                };

            return Result<GetProfileQueryResponse>
                .SuccessResult(response);
        }
        public async Task<Result> UpdateProfileAsync(
    UpdateProfileCommandRequest request)
        {
            var user =
                await _userManager.FindByIdAsync(
                    _currentUserService.UserId.ToString());

            if (user is null)
            {
                return Result.Failure(
                    "Kullanıcı bulunamadı.");
            }

            user.FirstName = request.FirstName;

            user.LastName = request.LastName;

            user.UserName = request.UserName;

            user.Email = request.Email;

            var result =
                await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Result.Failure(
                    result.Errors
                        .Select(x => x.Description)
                        .ToList());
            }

            return Result.SuccessResult(
                "Profil başarıyla güncellendi.");
        }
        public async Task<Result> ChangePasswordAsync(
    ChangePasswordCommandRequest request)
        {
            if (request.NewPassword != request.ConfirmPassword)
            {
                return Result.Failure(
                    "Yeni şifreler eşleşmiyor.");
            }

            var user =
                await _userManager.FindByIdAsync(
                    _currentUserService.UserId.ToString());

            if (user is null)
            {
                return Result.Failure(
                    "Kullanıcı bulunamadı.");
            }

            var result =
                await _userManager.ChangePasswordAsync(
                    user,
                    request.CurrentPassword,
                    request.NewPassword);

            if (!result.Succeeded)
            {
                return Result.Failure(
                    result.Errors
                        .Select(x => x.Description)
                        .ToList());
            }

            return Result.SuccessResult(
                "Şifreniz başarıyla değiştirildi.");
        }

    }
}