using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Commands
{
    public class CompletePurchaseOrderCommand
    : IRequest<Result>
    {
        public Guid Id { get; set; }

        public Guid WarehouseId { get; set; }
    }
}
