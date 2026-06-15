using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Contracts.Events
{
    public sealed class PurchaseOrderCompletedEvent
    {
        public Guid PurchaseOrderId { get; set; }

        public string OrderNumber { get; set; } = default!;
    }
}
