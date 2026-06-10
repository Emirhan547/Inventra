using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Users.Commands;
using Inventra.Application.Features.Users.Results;
using Inventra.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Identity
{
    public class UserService:IUserService
    {
        private readonly UserManager<AppUser>
            _userManager;

        private readonly RoleManager<AppRole>
            _roleManager;

        public UserService(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result<List<GetUsersQueryResponse>>>GetUsersAsync()
        {
            var users =await _userManager.Users.Select(user =>new GetUsersQueryResponse
                        {
                            Id = user.Id,
                            UserName = user.UserName!,
                            Email = user.Email!
                        })
                    .ToListAsync();

            return Result<
                List<GetUsersQueryResponse>>
                .SuccessResult(users);
        }
        public async Task<Result<GetUserByIdQueryResponse>>GetUserByIdAsync(Guid userId)
        {
            var user =await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                return Result<GetUserByIdQueryResponse>.Failure("Kullanıcı bulunamadı.");
            }
            var response =new GetUserByIdQueryResponse
                {
                    Id = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!
                };

            return Result< GetUserByIdQueryResponse>.SuccessResult(response);
        }
        public async Task<Result<GetUserRolesQueryResponse>>GetUserRolesAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                return Result<GetUserRolesQueryResponse>.Failure("Kullanıcı bulunamadı.");
            }

            var roles =await _userManager.GetRolesAsync(user);

            var response =new GetUserRolesQueryResponse
                {
                    Roles = roles.ToList()
                };

            return Result<GetUserRolesQueryResponse>.SuccessResult(response);
        }
        public async Task<Result>AssignRoleAsync(AssignRoleCommandRequest request)
        {
            if (!Roles.All.Contains(request.RoleName))
            {
                return Result.Failure("Geçersiz rol.");
            }
            var user =await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user is null)
            {
                return Result.Failure("Kullanıcı bulunamadı.");
            }

            var roleExists =await _roleManager.RoleExistsAsync(request.RoleName);

            if (!roleExists)
            {
                return Result.Failure("Rol bulunamadı.");
            }

            var isInRole =await _userManager.IsInRoleAsync(user,request.RoleName);

            if (isInRole)
            {
                return Result.Failure("Kullanıcı zaten bu role sahip.");
            }

            var result =await _userManager.AddToRoleAsync(user,request.RoleName);

            if (!result.Succeeded)
            {
                return Result.Failure(result.Errors.Select(x => x.Description).ToList());
            }

            return Result.SuccessResult();
        }
        public async Task<Result> RemoveRoleAsync(RemoveRoleCommandRequest request)
        {
            if (!Roles.All.Contains(request.RoleName))
            {
                return Result.Failure("Geçersiz rol.");
            }

            var user =await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user is null)
            {
                return Result.Failure("Kullanıcı bulunamadı.");
            }

            var roleExists =await _roleManager.RoleExistsAsync(request.RoleName);

            if (!roleExists)
            {
                return Result.Failure("Rol bulunamadı.");
            }

            var isInRole =await _userManager.IsInRoleAsync(user,request.RoleName);

            if (!isInRole)
            {
                return Result.Failure("Kullanıcı bu role sahip değil.");
            }

            
            if (request.RoleName == Roles.Admin)
            {
                var admins =await _userManager.GetUsersInRoleAsync(Roles.Admin);

                if (admins.Count <= 1)
                {
                    return Result.Failure("Sistemde en az bir admin bulunmalıdır.");
                }
            }

            var result =await _userManager.RemoveFromRoleAsync(user, request.RoleName);

            if (!result.Succeeded)
            {
                return Result.Failure(result.Errors.Select(x => x.Description).ToList());
            }

            return Result.SuccessResult();
        }
    }
}
