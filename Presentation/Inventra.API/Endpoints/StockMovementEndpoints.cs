using Inventra.Application.Features.StockMovements.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.API.Endpoints;

public static class StockMovementEndpoints
{
    public static void MapStockMovementEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group =
            app.MapGroup("/stock-movements")
               .WithTags("Stock Movements");

        group.MapGet(
            "/",
            async (
                [AsParameters]
                GetStockMovementsQuery request,
                IMediator mediator)
                => await mediator.Send(request))
            .RequireAuthorization();
    }
}