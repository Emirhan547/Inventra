using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Suppliers.Results
{
    public class GetSupplierByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
