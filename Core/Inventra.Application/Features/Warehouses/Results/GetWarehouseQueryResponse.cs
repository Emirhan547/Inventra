using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Warehouses.Results
{
    public class GetWarehouseQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }
    }
}
