using Inventra.Application.Features.Suppliers.Commands;
using Inventra.Application.Features.Suppliers.Queries;
using MediatR;

namespace Inventra.API.Endpoints;

public static class SupplierEndpoints
{
    public static void MapSupplierEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/suppliers")
            .WithTags("Suppliers");

        group.MapPost("/",
            async (
                CreateSupplierCommandRequest request,
                IMediator mediator)
                => await mediator.Send(request));

        group.MapGet("/",
            async (
                IMediator mediator)
                => await mediator.Send(
                    new GetSuppliersQueryRequest()));

        group.MapGet("/{id:guid}",
            async (
                Guid id,
                IMediator mediator)
                => await mediator.Send(
                    new GetSupplierByIdQueryRequest(id)));
                    

        group.MapPut("/{id:guid}",
            async (
                Guid id,
                UpdateSupplierCommand command,
                IMediator mediator) =>
            {
                command.Id = id;

                return await mediator.Send(command);
            });

        group.MapDelete("/{id:guid}",
            async (
                Guid id,
                IMediator mediator)
                => await mediator.Send(
                    new RemoveSupplierCommand(id)));
                    
    }
}