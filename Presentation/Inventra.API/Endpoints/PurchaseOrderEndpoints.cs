using Inventra.Application.Features.PurchaseOrders.Commands;
using Inventra.Application.Features.PurchaseOrders.Queries;
using Inventra.Domain.Constants;
using MediatR;

namespace Inventra.API.Endpoints;

public static class PurchaseOrderEndpoints
{
    public static void MapPurchaseOrderEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group =
            app.MapGroup("/purchase-orders")
               .WithTags("Purchase Orders");

        group.MapPost(
            "/",
            async (
                CreatePurchaseOrderCommandRequest request,
                IMediator mediator)
                => await mediator.Send(request))
            .RequireAuthorization(
                Policies.PurchaseOrderCreate);

        group.MapGet(
            "/",
            async (
                IMediator mediator)
                => await mediator.Send(
                    new GetPurchaseOrdersQueryRequest()))
            .RequireAuthorization();

        group.MapGet(
            "/{id:guid}",
            async (
                Guid id,
                IMediator mediator)
                => await mediator.Send(
                    new GetPurchaseOrderByIdQueryRequest
                    {
                        Id = id
                    }))
            .RequireAuthorization();

        group.MapPatch(
            "/{id:guid}/approve",
            async (
                Guid id,
                IMediator mediator)
                => await mediator.Send(
                    new ApprovePurchaseOrderCommand
                    {
                        Id = id
                    }))
            .RequireAuthorization(
                Policies.PurchaseOrderApprove);

        group.MapPatch(
            "/{id:guid}/complete",
            async (
                Guid id,
                Guid warehouseId,
                IMediator mediator)
                => await mediator.Send(
                    new CompletePurchaseOrderCommand
                    {
                        Id = id,
                        WarehouseId = warehouseId
                    }))
            .RequireAuthorization(
                Policies.PurchaseOrderComplete);
    }
}