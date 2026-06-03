using Inventra.Application.Abstractions.Repositories.StockMovementRepositories;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.StockMovementRepositories
{
    public class StockMovementWriteRepository : WriteRepository<StockMovement>, IStockMovementWriteRepository
    {
        public StockMovementWriteRepository(InventraDbContext context) : base(context)
        {
        }
    }
}
