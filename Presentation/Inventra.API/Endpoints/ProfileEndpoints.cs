using Inventra.Application.Features.Profiles.Commands;
using Inventra.Application.Features.Profiles.Queries;
using MediatR;

namespace Inventra.API.Endpoints
{
    public static class ProfileEndpoints
    {
        public static void MapProfileEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group =
                app.MapGroup("/profile")
                   .RequireAuthorization();

            group.MapGet(
                "",
                async (ISender sender) =>
                {
                    var result =
                        await sender.Send(
                            new GetProfileQuery());

                    return Results.Ok(result);
                });
            group.MapPut(
    "",
    async (
        UpdateProfileCommandRequest request,
        ISender sender) =>
    {
        var result =
            await sender.Send(request);

        return Results.Ok(result);
    });
            group.MapPut(
    "/change-password",
    async (
        ChangePasswordCommandRequest request,
        ISender sender) =>
    {
        var result =
            await sender.Send(request);

        return Results.Ok(result);
    });


        }
    }
}