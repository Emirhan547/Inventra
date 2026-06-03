using Inventra.Application.Abstractions.Repositories.WarehouseRepositories;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.WarehouseRepositories
{
    public class WarehouseWriteRepository : WriteRepository<Warehouse>, IWarehouseWriteRepository
    {
        public WarehouseWriteRepository(InventraDbContext context) : base(context)
        {
        }
    }
}
