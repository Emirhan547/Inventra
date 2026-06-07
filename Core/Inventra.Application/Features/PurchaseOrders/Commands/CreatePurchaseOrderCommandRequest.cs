using Inventra.Application.Common.Results;
using Inventra.Application.Features.PurchaseOrderItems.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Commands
{
    public class CreatePurchaseOrderCommandRequest
     : IRequest<Result<CreatePurchaseOrderCommandResponse>>
    {
        public Guid SupplierId { get; set; }

        public List<CreatePurchaseOrderItemRequest> Items
        { get; set; } = [];
    }
}
