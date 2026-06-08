using Inventra.Application.Features.Auths.Logins;
using Inventra.Application.Features.Auths.Registers;
using MediatR;

namespace Inventra.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/auth").WithTags("Auth");

            group.MapPost("/register", async (RegisterCommandRequest request, IMediator mediator) =>
                {
                    var result = await mediator.Send(request);

                    if (!result.Success)
                    {
                        return Results.BadRequest(result);
                    }

                    return Results.Ok(result);
                });

            group.MapPost("/login", async (LoginCommandRequest request, IMediator mediator) =>
                {
                    var result = await mediator.Send(request);

                    if (!result.Success)
                    {
                        return Results.BadRequest(result);
                    }

                    return Results.Ok(result);
                });
        }
    }
}