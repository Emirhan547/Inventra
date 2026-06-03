using Inventra.Application.Abstractions.Repositories.SupplierRepositories;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.SupplierRepositories
{
    public class SupplierWriteRepository : WriteRepository<Supplier>, ISupplierWriteRepository
    {
        public SupplierWriteRepository(InventraDbContext context) : base(context)
        {
        }
    }
}
