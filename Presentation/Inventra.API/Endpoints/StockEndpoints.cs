using Inventra.Application.Features.Stocks.Commands;
using Inventra.Application.Features.Stocks.Queries;
using MediatR;

namespace Inventra.API.Endpoints;

public static class StockEndpoints
{
    public static void MapStockEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/stocks").WithTags("Stocks");

        group.MapPost("/in",async ( StockInCommand command,IMediator mediator)
                => await mediator.Send(command));
        group.MapGet("/",async (IMediator mediator)
        => await mediator.Send(new GetStocksQuery()));
        group.MapPost("/out",async (StockOutCommand command,IMediator mediator)=> await mediator.Send(command));
        group.MapPost("/transfer",async (TransferStockCommand command,IMediator mediator)
        => await mediator.Send(command));
    }
}