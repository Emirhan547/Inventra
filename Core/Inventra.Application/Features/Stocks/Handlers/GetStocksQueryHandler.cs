using Inventra.Application.Abstractions.Repositories.StockRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Stocks.Queries;
using Inventra.Application.Features.Stocks.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Stocks.Handlers
{
    public class GetStocksQueryHandler
    : IRequestHandler<
        GetStocksQuery,
        Result<List<GetStocksQueryResponse>>>
    {
        private readonly IStockReadRepository
            _stockReadRepository;

        public GetStocksQueryHandler(
            IStockReadRepository stockReadRepository)
        {
            _stockReadRepository =
                stockReadRepository;
        }

        public async Task<
            Result<List<GetStocksQueryResponse>>>
            Handle(
                GetStocksQuery request,
                CancellationToken cancellationToken)
        {
            var stocks =
                await _stockReadRepository
                    .GetStocksAsync(
                        cancellationToken);

            return Result<
                List<GetStocksQueryResponse>>
                .SuccessResult(stocks);
        }
    }
}
