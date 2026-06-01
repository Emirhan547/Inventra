using Inventra.Domain.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Entities
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public List<Stock> Stocks { get; set; }
            
    }
}
