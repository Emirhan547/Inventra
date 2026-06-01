using Inventra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Entities
{
    public class Stock : BaseEntity
    {
        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public Guid WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }

        public int Quantity { get; set; }

        public List<StockMovement> StockMovements{ get; set; } 
    }
}
