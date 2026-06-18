using Inventra.Application.Common.Results;
using Inventra.Application.Features.PurchaseOrders.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Queries
{
    public sealed class GetPurchaseOrderAiAnalysisQueryRequest
     : IRequest<Result<GetPurchaseOrderAiAnalysisQueryResponse>>
    {
        public Guid Id { get; set; }
    }
}
