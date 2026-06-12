using Inventra.Application.Common.Pagination;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.PurchaseOrders.Results;
using Inventra.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Queries
{
    public sealed class GetPurchaseOrdersQueryRequest: PagedRequest,IRequest<Result<PagedResponse<GetPurchaseOrdersQueryResponse>>>
    {
        public PurchaseOrderStatus? Status { get; set; }

        public Guid? SupplierId { get; set; }
    }
}
