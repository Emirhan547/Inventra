using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Contracts.Events
{
    public sealed class PurchaseOrderCreatedEvent
    {
        public Guid PurchaseOrderId { get; set; }

        public string OrderNumber { get; set; } = default!;
    }
}
