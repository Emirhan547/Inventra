using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.LoginDtos;
using Inventra.WebUI.Dtos.RegisterDtos;

namespace Inventra.WebUI.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponseDto>>LoginAsync(LoginDto dto,HttpContext httpContext);

        Task<ApiResponse<object>>RegisterAsync(RegisterDto dto);

        Task LogoutAsync(HttpContext httpContext);
    }
}
