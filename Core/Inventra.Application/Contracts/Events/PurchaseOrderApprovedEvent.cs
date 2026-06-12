using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Contracts.Events
{
    public sealed class PurchaseOrderApprovedEvent
    {
        public Guid PurchaseOrderId { get; set; }

        public string OrderNumber { get; set; } = default!;

        public Guid UserId { get; set; }

        public string UserName { get; set; } = default!;
    }
}
