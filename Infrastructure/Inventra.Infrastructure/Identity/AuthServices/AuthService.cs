using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Infrastructures.JwtServices;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Auths.Logins;
using Inventra.Application.Features.Auths.Registers;
using Inventra.Application.Features.Auths.Tokens;
using Microsoft.AspNetCore.Identity;
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
            var token =_tokenService.CreateToken(new TokenUser
                    {
                        Id = user.Id,
                        UserName = user.UserName!,
                        Email = user.Email!
                    });
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
            return Result.SuccessResult("Kullanıcı başarıyla oluşturuldu.");
        }
    }
}
