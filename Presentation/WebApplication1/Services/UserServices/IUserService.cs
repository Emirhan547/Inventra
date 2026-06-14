using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.UserDtos;

namespace Inventra.WebUI.Services.UserServices
{
    public interface IUserService
    {
        Task<PagedResponse<ResultUserDto>>GetAllAsync(UserFilterDto filter);
        Task<UserDetailDto?>GetByIdAsync(Guid id);
        Task<UserRolesDto?>GetRolesAsync(Guid id);
        Task AssignRoleAsync(Guid userId,string roleName);
        Task RemoveRoleAsync(Guid userId,string roleName);
    }
}
