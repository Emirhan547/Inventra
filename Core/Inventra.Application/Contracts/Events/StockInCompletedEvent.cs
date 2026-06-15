using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Contracts.Events
{
    public sealed class StockInCompletedEvent
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = default!;

        public int Quantity { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; } = default!;
    }
}
