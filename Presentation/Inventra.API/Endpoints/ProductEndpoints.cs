using Inventra.Application.Features.Products.Commands;
using Inventra.Application.Features.Products.Queries;
using MediatR;

namespace Inventra.API.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products")
            .WithTags("Products");

        group.MapPost("/",
            async (
                CreateProductCommandRequest request,
                IMediator mediator) =>
            {
                var result =
                    await mediator.Send(request);

                if (!result.Success)
                {
                    return Results.BadRequest(result);
                }

                return Results.Created(
                    $"/products/{result.Data}",
                    result);
            });

        group.MapGet("/",
            async (
                IMediator mediator) =>
            {
                var result = await mediator.Send(
                    new GetProductsQueryRequest());

                return Results.Ok(result);
            });

        group.MapGet("/{id:guid}",
            async (
                Guid id,
                IMediator mediator) =>
            {
                var result =
                    await mediator.Send(
                        new GetProductByIdQueryRequest
                        {
                            Id = id
                        });

                if (!result.Success)
                {
                    return Results.NotFound(result);
                }

                return Results.Ok(result);
            });

        group.MapPut("/{id:guid}",
            async (
                Guid id,
                UpdateProductCommand command,
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
            });

        group.MapDelete("/{id:guid}",
            async (
                Guid id,
                IMediator mediator) =>
            {
                var result =
                    await mediator.Send(
                        new RemoveProductCommand
                        {
                            Id = id
                        });

                if (!result.Success)
                {
                    return Results.NotFound(result);
                }

                return Results.Ok(result);
            });
    }
}