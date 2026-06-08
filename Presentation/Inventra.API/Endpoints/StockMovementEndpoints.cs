using Inventra.Application.Features.StockMovements.Queries;
using MediatR;

namespace Inventra.API.Endpoints
{
    public static class StockMovementEndpoints
    {
        public static void MapStockMovementEndpoints(this IEndpointRouteBuilder app)
        {
            var group =app.MapGroup("/stock-movements").WithTags("Stock Movements");

            group.MapGet("/",async (IMediator mediator)=> await mediator.Send(new GetStockMovementsQuery()));
        }
    }
}
