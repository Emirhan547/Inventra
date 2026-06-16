using Inventra.Application.Abstractions.Infrastructures.AI;
using Inventra.Application.Abstractions.Repositories.StockRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Stocks.Queries;
using Inventra.Application.Features.Stocks.Results;
using MediatR;

namespace Inventra.Application.Features.Stocks.Handlers;

public class GetStocksQueryHandler
: IRequestHandler<
GetStocksQuery,
Result<PagedResponse<GetStocksQueryResponse>>>
{
    private readonly IStockReadRepository
    _stockReadRepository;

private readonly IStockForecastService
    _stockForecastService;

    public GetStocksQueryHandler(
        IStockReadRepository stockReadRepository,
        IStockForecastService stockForecastService)
    {
        _stockReadRepository =
            stockReadRepository;

        _stockForecastService =
            stockForecastService;
    }

    public async Task<
        Result<PagedResponse<GetStocksQueryResponse>>>
        Handle(
            GetStocksQuery request,
            CancellationToken cancellationToken)
    {
        var pagedStocks =
            await _stockReadRepository
                .GetPagedStocksAsync(
                    request.PageNumber,
                    request.PageSize,
                    cancellationToken);

        var stockItems =
            new List<GetStocksQueryResponse>();

        foreach (var stock in pagedStocks.Items)
        {
            var history =
                await _stockReadRepository
                    .GetForecastHistoryAsync(
                        stock.ProductId,
                        cancellationToken);

            Console.WriteLine(
                $"==============================");

            Console.WriteLine(
                $"Ürün: {stock.Product.Name}");

            Console.WriteLine(
                $"History Count: {history.Count}");

            Console.WriteLine(
                $"Average: {(history.Any() ? history.Average() : 0)}");

            Console.WriteLine(
                $"Total: {history.Sum()}");

            Console.WriteLine(
                $"Min: {(history.Any() ? history.Min() : 0)}");

            Console.WriteLine(
                $"Max: {(history.Any() ? history.Max() : 0)}");

            var prediction =
                _stockForecastService
                    .PredictNext30Days(
                        history);

            Console.WriteLine(
                $"Prediction: {prediction}");

            var recommendedOrder =
                Math.Max(
                    prediction - stock.Quantity,
                    0);

            var risk =
                prediction > stock.Quantity
                    ? "High"
                    : prediction >
                      stock.Quantity * 0.7
                        ? "Medium"
                        : "Low";

            stockItems.Add(
                new GetStocksQueryResponse
                {
                    Id = stock.Id,

                    ProductId =
                        stock.ProductId,

                    ProductName =
                        stock.Product.Name,

                    WarehouseId =
                        stock.WarehouseId,

                    WarehouseName =
                        stock.Warehouse.Name,

                    Quantity =
                        stock.Quantity,

                    PredictedConsumption30Days =
                        prediction,

                    RiskLevel =
                        risk,

                    RecommendedOrderQuantity =
                        recommendedOrder
                });
        }

        var response =
            new PagedResponse<GetStocksQueryResponse>
            {
                Items =
                    stockItems,

                PageNumber =
                    pagedStocks.PageNumber,

                PageSize =
                    pagedStocks.PageSize,

                TotalCount =
                    pagedStocks.TotalCount,

                TotalPages =
                    pagedStocks.TotalPages
            };

        return Result<
            PagedResponse<GetStocksQueryResponse>>
            .SuccessResult(response);
    }

}
