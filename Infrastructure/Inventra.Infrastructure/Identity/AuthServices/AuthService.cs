using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Infrastructures.JwtServices;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Auths.Logins;
using Inventra.Application.Features.Auths.Registers;
using Inventra.Application.Features.Auths.Tokens;
using Inventra.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Identity.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser>_userManager;
        private readonly SignInManager<AppUser>_signInManager;
        private readonly ITokenService _tokenService;
        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        public async Task<Result<LoginCommandResponse>>LoginAsync(LoginCommandRequest request)
        {
            var user =await _userManager.FindByNameAsync(request.UserName);
            if (user is null)
            {
                return Result<LoginCommandResponse>.Failure("Kullanıcı bulunamadı.");
            }
            var signInResult =await _signInManager.CheckPasswordSignInAsync(user,request.Password,false);

            if (!signInResult.Succeeded)
            {
                return Result<LoginCommandResponse>.Failure("Kullanıcı adı veya şifre hatalı.");
            }
            var roles =await _userManager.GetRolesAsync(user);
            var token =_tokenService.CreateToken(new TokenUser
                    {
                        Id = user.Id,
                        UserName = user.UserName!,
                        Email = user.Email!,
                         Roles = roles
            });
            var refreshToken =_tokenService.CreateRefreshToken();

            user.RefreshToken =refreshToken;

            user.RefreshTokenExpireDate =DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(user);

            token.RefreshToken =refreshToken;
            return Result< LoginCommandResponse>.SuccessResult(token);
        }

        public async Task<Result> RegisterAsync(RegisterCommandRequest request)
        {
            var exists =await _userManager.FindByNameAsync(request.UserName);
            if (exists is not null)
            {
                return Result.Failure("Bu kullanıcı adı kullanılmaktadır.");
            }
            var emailExists =await _userManager.FindByEmailAsync(request.Email);
            if (emailExists is not null)
            {
                return Result.Failure("Bu e-posta kullanılmaktadır.");
            }
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                FirstName =request.FirstName,
                LastName =request.LastName,
                UserName =request.UserName,
                Email =request.Email
            };

            var result =await _userManager.CreateAsync(user,request.Password);

            if (!result.Succeeded)
            {
                return Result.Failure(string.Join(Environment.NewLine,result.Errors.Select(x => x.Description)));
            }
            await _userManager.AddToRoleAsync(user,Roles.Employee);
            return Result.SuccessResult("Kullanıcı başarıyla oluşturuldu.");
        }
        public async Task<Result<LoginCommandResponse>>RefreshTokenAsync(RefreshTokenCommandRequest request)
        {
            var user =await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken ==request.RefreshToken);
            if (user is null)
            {
                return Result<LoginCommandResponse>.Failure("Geçersiz refresh token.");
            }

            if (user.RefreshTokenExpireDate <DateTime.UtcNow)
            {
                return Result<LoginCommandResponse>.Failure("Refresh token süresi dolmuş.");
            }
            var roles =await _userManager.GetRolesAsync(user);

            var token =_tokenService.CreateToken(
                    new TokenUser
                    {
                        Id = user.Id,
                        UserName = user.UserName!,
                        Email = user.Email!,
                        Roles = roles
                    });

            var newRefreshToken = _tokenService.CreateRefreshToken();
            user.RefreshToken =newRefreshToken;
            user.RefreshTokenExpireDate =DateTime.UtcNow.AddDays(7);
            await _userManager .UpdateAsync(user);
            token.RefreshToken =newRefreshToken;
            return Result<LoginCommandResponse>.SuccessResult(token);
        }
    }
}
