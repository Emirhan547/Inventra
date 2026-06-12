using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Messaging;
using Inventra.Application.Common.Pagination;
using Inventra.Application.Common.Results;
using Inventra.Application.Contracts.Events;
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
        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEventBus _eventBus;
        private readonly ICurrentUserService _currentUserService;
        public UserService(
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IEventBus eventBus,
            ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _eventBus = eventBus;
            _currentUserService = currentUserService;
        }

        public async Task<Result<PagedResponse<GetUsersQueryResponse>>>GetUsersAsync(int pageNumber,int pageSize,string? search)
        {
            IQueryable<AppUser> query =_userManager.Users;

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>x.UserName!.Contains(search) ||x.Email!.Contains(search));
            }
            var totalCount =await query.CountAsync();

            var users = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(user =>
                    new GetUsersQueryResponse
                    {
                        Id = user.Id,
                        UserName = user.UserName!,
                        Email = user.Email!
                    })
                .ToListAsync();

            var response =new PagedResponse<GetUsersQueryResponse>
                {
                    Items = users,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages =(int)Math.Ceiling(totalCount /(double)pageSize)
                };

            return Result<PagedResponse<GetUsersQueryResponse>>.SuccessResult(response);
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
            await _eventBus.PublishAsync(
    new RoleAssignedEvent
    {
        UserId = user.Id,
        UserName = user.UserName!,
        RoleName = request.RoleName,

        AssignedByUserId = _currentUserService.UserId,
        AssignedByUserName = _currentUserService.UserName
    });

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
