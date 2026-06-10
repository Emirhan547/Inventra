using Inventra.Application.Features.Warehouses.Commands;
using Inventra.Application.Features.Warehouses.Queries;
using Inventra.Domain.Constants;
using MediatR;

namespace Inventra.API.Endpoints
{
    public static class WarehouseEndpoints
    {
        public static  void MapWarehouseEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/warehouses").WithTags("Warehouses");

            group.MapPost(
    "/",
    async (
        CreateWarehouseCommandRequest request,
        IMediator mediator) =>
    {
        var result =
            await mediator.Send(request);

        if (!result.Success)
        {
            return Results.BadRequest(result);
        }

        return Results.Created(
            $"/warehouses/{result.Data}",
            result);
    })
    .RequireAuthorization(
        Policies.WarehouseManagement);

            group.MapGet(
                "/",
                async (
                    IMediator mediator) =>
                {
                    var result =
                        await mediator.Send(
                            new GetWarehouseQueryRequest());

                    return Results.Ok(result);
                })
                .RequireAuthorization();

            group.MapGet(
                "/{id:guid}",
                async (
                    Guid id,
                    IMediator mediator) =>
                {
                    var result =
                        await mediator.Send(
                            new GetWarehouseByIdQueryRequest
                            {
                                Id = id
                            });

                    if (!result.Success)
                    {
                        return Results.NotFound(result);
                    }

                    return Results.Ok(result);
                })
                .RequireAuthorization();

            group.MapPut(
                "/{id:guid}",
                async (
                    Guid id,
                    UpdateWarehouseCommand command,
                    IMediator mediator) =>
                {
                    command.Id = id;

                    var result =
                        await mediator.Send(command);

                    if (!result.Success)
                    {
                        return Results.BadRequest(result);
                    }

                    return Results.Ok(result);
                })
                .RequireAuthorization(
                    Policies.WarehouseManagement);

            group.MapDelete(
                "/{id:guid}",
                async (
                    Guid id,
                    IMediator mediator) =>
                {
                    var result =
                        await mediator.Send(
                            new RemoveWarehouseCommand
                            {
                                Id = id
                            });

                    if (!result.Success)
                    {
                        return Results.NotFound(result);
                    }

                    return Results.Ok(result);
                })
                .RequireAuthorization(
                    Policies.WarehouseManagement);
        }
    }
}
