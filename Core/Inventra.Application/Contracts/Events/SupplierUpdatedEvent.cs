using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Contracts.Events
{
    public sealed class SupplierUpdatedEvent
    {
        public Guid SupplierId { get; set; }

        public string SupplierName { get; set; } = default!;

        public Guid UserId { get; set; }

        public string UserName { get; set; } = default!;
    }
}
