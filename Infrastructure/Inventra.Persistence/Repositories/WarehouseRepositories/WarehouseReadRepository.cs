using Inventra.Application.Abstractions.Repositories.WarehouseRepositories;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.WarehouseRepositories
{
    public class WarehouseReadRepository : ReadRepository<Warehouse>, IWarehouseReadRepository
    {
        public WarehouseReadRepository(InventraDbContext context) : base(context)
        {
        }
    }
}
