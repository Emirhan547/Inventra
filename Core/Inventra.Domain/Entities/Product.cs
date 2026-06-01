using Inventra.Domain.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Entities
{
    public class Product:BaseEntity
    {
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public string Name { get; set; }

        public string SKU { get; set; }

        public decimal UnitPrice { get; set; }

        public int MinimumStockLevel { get; set; }

        public List<Stock> Stocks { get; set; }
            

        public List<PurchaseOrderItem> PurchaseOrderItems{ get; set; } 
    }
}
