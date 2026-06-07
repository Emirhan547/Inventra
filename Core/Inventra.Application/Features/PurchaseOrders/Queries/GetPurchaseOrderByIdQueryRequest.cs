using Inventra.Application.Common.Results;
using Inventra.Application.Features.PurchaseOrders.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Queries
{
    public class GetPurchaseOrderByIdQueryRequest : IRequest<
        Result<GetPurchaseOrderByIdQueryResponse>>
    {
        public Guid Id { get; set; }

    }
}
