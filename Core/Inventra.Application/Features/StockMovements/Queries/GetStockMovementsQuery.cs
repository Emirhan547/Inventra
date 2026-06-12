using Inventra.Application.Common.Pagination;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.StockMovements.Results;
using Inventra.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.StockMovements.Queries
{
    public sealed class GetStockMovementsQuery: PagedRequest,IRequest<Result<PagedResponse<GetStockMovementsQueryResponse>>>
    {
        public StockMovementType? Type { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
