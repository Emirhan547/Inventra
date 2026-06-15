using Inventra.Application.Features.Stocks.Commands;
using Inventra.Application.Features.Stocks.Queries;
using Inventra.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.API.Endpoints;

public static class StockEndpoints
{
    public static void MapStockEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group =
            app.MapGroup("/stocks")
               .WithTags("Stocks");

        group.MapGet(
            "/",
            async (
                [AsParameters]
                GetStocksQuery request,
                IMediator mediator)
                => await mediator.Send(request))
            .RequireAuthorization();

        group.MapPost(
            "/in",
            async (
                StockInCommand command,
                IMediator mediator)
                => await mediator.Send(command))
            .RequireAuthorization(
                Policies.StockOperation);

        group.MapPost(
            "/out",
            async (
                StockOutCommand command,
                IMediator mediator)
                => await mediator.Send(command))
            .RequireAuthorization(
                Policies.StockOperation);

        group.MapPost(
            "/transfer",
            async (
                TransferStockCommand command,
                IMediator mediator)
                => await mediator.Send(command))
            .RequireAuthorization(
                Policies.StockOperation);
    }
}