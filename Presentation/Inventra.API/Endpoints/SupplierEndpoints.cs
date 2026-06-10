using Inventra.Application.Features.Suppliers.Commands;
using Inventra.Application.Features.Suppliers.Queries;
using Inventra.Domain.Constants;
using MediatR;

namespace Inventra.API.Endpoints;

public static class SupplierEndpoints
{
    public static void MapSupplierEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group =
            app.MapGroup("/suppliers")
               .WithTags("Suppliers");

        group.MapPost(
            "/",
            async (
                CreateSupplierCommandRequest request,
                IMediator mediator)
                    => await mediator.Send(request))
            .RequireAuthorization(
                Policies.SupplierManagement);

        group.MapGet(
            "/",
            async (
                IMediator mediator)
                    => await mediator.Send(
                        new GetSuppliersQueryRequest()))
            .RequireAuthorization();

        group.MapGet(
            "/{id:guid}",
            async (
                Guid id,
                IMediator mediator)
                    => await mediator.Send(
                        new GetSupplierByIdQueryRequest(id)))
            .RequireAuthorization();

        group.MapPut(
            "/{id:guid}",
            async (
                Guid id,
                UpdateSupplierCommand command,
                IMediator mediator) =>
            {
                command.Id = id;

                return await mediator.Send(command);
            })
            .RequireAuthorization(
                Policies.SupplierManagement);

        group.MapDelete(
            "/{id:guid}",
            async (
                Guid id,
                IMediator mediator)
                    => await mediator.Send(
                        new RemoveSupplierCommand(id)))
            .RequireAuthorization(
                Policies.SupplierManagement);
    }
}