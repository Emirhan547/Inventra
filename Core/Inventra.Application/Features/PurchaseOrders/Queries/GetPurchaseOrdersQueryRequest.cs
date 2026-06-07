using Inventra.Application.Common.Results;
using Inventra.Application.Features.PurchaseOrders.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Queries
{
    public class GetPurchaseOrdersQueryRequest : IRequest<
        Result<List<GetPurchaseOrdersQueryResponse>>>
    {
    }
}
