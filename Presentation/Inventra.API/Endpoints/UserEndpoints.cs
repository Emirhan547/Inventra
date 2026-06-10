using Inventra.Application.Features.Users.Commands;
using Inventra.Application.Features.Users.Queries;
using Inventra.Domain.Constants;
using MediatR;

namespace Inventra.API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/users").WithTags("Users");

            group.MapGet("/",async (IMediator mediator) =>await mediator.Send(new GetUsersQueryRequest()))
                .RequireAuthorization(
                    Policies.UserManagement);

            group.MapGet("/{id:guid}",async (Guid id,IMediator mediator) =>await mediator.Send(
                        new GetUserByIdQueryRequest
                        {
                            UserId = id
                        }))
                .RequireAuthorization(
                    Policies.UserManagement);

            group.MapGet("/{id:guid}/roles",async (Guid id,IMediator mediator) =>
                    await mediator.Send(
                        new GetUserRolesQueryRequest
                        {
                            UserId = id
                        }))
                .RequireAuthorization(
                    Policies.UserManagement);

            group.MapPost("/{id:guid}/roles",async (Guid id,AssignRoleCommandRequest request,IMediator mediator) =>
                {
                    request.UserId = id;

                    return await mediator.Send(
                        request);
                })
                .RequireAuthorization(
                    Policies.UserManagement);

            group.MapDelete("/{id:guid}/roles/{roleName}",async (Guid id,string roleName,IMediator mediator) =>
    {
        var request =new RemoveRoleCommandRequest
            {
                UserId = id,
                RoleName = roleName
            };

        return await mediator.Send(request);
    })
    .RequireAuthorization(
        Policies.UserManagement);
        }
    }
}