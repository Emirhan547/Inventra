using Inventra.Application.Common.Results;
using Inventra.Application.Features.Stocks.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Stocks.Queries
{
    public class GetStocksQuery
     : IRequest<Result<List<GetStocksQueryResponse>>>
    {
    }
}
