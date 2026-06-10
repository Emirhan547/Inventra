using Inventra.Application.Common.Results;
using Inventra.Application.Features.Users.Commands;
using Inventra.Application.Features.Users.Results;

namespace Inventra.Application.Abstractions.Infrastructures.IdentityServices
{
    public interface IUserService
    {
        Task<Result<List<GetUsersQueryResponse>>>GetUsersAsync();
        Task<Result<GetUserByIdQueryResponse>>GetUserByIdAsync(Guid userId);
        Task<Result<GetUserRolesQueryResponse>>GetUserRolesAsync(Guid userId);
        Task<Result>AssignRoleAsync(AssignRoleCommandRequest request);
        Task<Result>RemoveRoleAsync(RemoveRoleCommandRequest request);
    }
}
