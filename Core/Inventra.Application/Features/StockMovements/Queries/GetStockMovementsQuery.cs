using Inventra.Application.Common.Results;
using Inventra.Application.Features.StockMovements.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.StockMovements.Queries
{
    public class GetStockMovementsQuery
    : IRequest<Result<List<GetStockMovementsQueryResponse>>>
    {
    }
}
