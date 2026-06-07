using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Commands
{
    public class ApprovePurchaseOrderCommand
    : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
