using Inventra.Application.Abstractions.Repositories.StockRepositories;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.StockRepositories
{
    public class StockWriteRepository : WriteRepository<Stock>, IStockWriteRepository
    {
        public StockWriteRepository(InventraDbContext context) : base(context)
        {
        }
    }
}
