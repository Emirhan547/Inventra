using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Contracts.Events
{
    public sealed class LowStockDetectedEvent
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int CurrentQuantity { get; set; }

        public int MinimumStockLevel { get; set; }
    }
}
