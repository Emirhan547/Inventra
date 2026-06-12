using Inventra.Application.Common.Pagination;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Stocks.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Stocks.Queries
{
    public sealed class GetStocksQuery: PagedRequest,IRequest< Result<PagedResponse<GetStocksQueryResponse>>>
    {
    }
}
