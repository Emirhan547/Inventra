using System.Net.Http.Json;
using System.Security.Claims;
using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.LoginDtos;
using Inventra.WebUI.Dtos.RegisterDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Inventra.WebUI.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _client;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _client =httpClientFactory.CreateClient("InventraApi");
        }

        public async Task<ApiResponse<LoginResponseDto>>LoginAsync(LoginDto dto,HttpContext httpContext)
        {
            var response =await _client.PostAsJsonAsync("auth/login",dto);
            var result =await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponseDto>>();

            if (result is null || !result.Success)
            {
                return result ??new ApiResponse<LoginResponseDto>
                       {
                           Success = false,
                           Message = "Giriş işlemi başarısız."
                       };
            }
            var claims = new List<Claim>
{
    new Claim(
        ClaimTypes.Name,
        dto.UserName)
};

            foreach (var role in result.Data!.Roles)
            {
                claims.Add(
                    new Claim(
                        ClaimTypes.Role,
                        role));
            }
            var identity =new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var principal =new ClaimsPrincipal(identity);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);

            httpContext.Response.Cookies.Append("AccessToken",result.Data!.AccessToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires =result.Data.ExpirationTime
                });

            httpContext.Response.Cookies.Append("RefreshToken",result.Data.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

            return result;
        }
        public async Task<ApiResponse<object>>RegisterAsync(RegisterDto dto)
        {
            var response =await _client.PostAsJsonAsync("auth/register",dto);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
            return result ??new ApiResponse<object>
                   {
                       Success = false,
                       Message = "Kayıt işlemi başarısız."
                   };
        }
        public async Task LogoutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            httpContext.Response.Cookies.Delete("AccessToken");
            httpContext.Response.Cookies.Delete("RefreshToken");
        }
    }
}