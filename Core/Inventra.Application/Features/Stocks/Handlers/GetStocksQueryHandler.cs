using Inventra.Application.Abstractions.Repositories.StockRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Stocks.Queries;
using Inventra.Application.Features.Stocks.Results;
using MediatR;

namespace Inventra.Application.Features.Stocks.Handlers
{
    public class GetStocksQueryHandler: IRequestHandler<GetStocksQuery,Result<PagedResponse< GetStocksQueryResponse>>>
    {
        private readonly IStockReadRepository _stockReadRepository;

        public GetStocksQueryHandler(IStockReadRepository stockReadRepository)
        {
            _stockReadRepository =stockReadRepository;
        }

        public async Task<Result<PagedResponse<GetStocksQueryResponse>>>Handle(GetStocksQuery request,CancellationToken cancellationToken)
        {
            var pagedStocks =await _stockReadRepository.GetPagedStocksAsync(request.PageNumber,request.PageSize, cancellationToken);

            var response = new PagedResponse< GetStocksQueryResponse>
                {
                    Items = pagedStocks.Items.Select(x => new GetStocksQueryResponse
                        {
                            Id = x.Id,
                            ProductId = x.ProductId,
                            ProductName = x.Product.Name,
                            WarehouseId = x.WarehouseId,
                            WarehouseName = x.Warehouse.Name,
                            Quantity = x.Quantity
                        })
                        .ToList(),

                    PageNumber =pagedStocks.PageNumber,

                    PageSize =pagedStocks.PageSize,

                    TotalCount = pagedStocks.TotalCount,

                    TotalPages =pagedStocks.TotalPages
                };

            return Result<PagedResponse< GetStocksQueryResponse>>.SuccessResult(response);
        }
    }
}