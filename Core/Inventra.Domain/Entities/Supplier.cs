using Inventra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public List<PurchaseOrder> PurchaseOrders{ get; set; } 
    }
}
