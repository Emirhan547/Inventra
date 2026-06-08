using Inventra.Application.Abstractions.Repositories.StockRepositories;
using Inventra.Application.Features.Stocks.Results;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.StockRepositories
{
    public class StockReadRepository : ReadRepository<Stock>, IStockReadRepository
    {
        public StockReadRepository(InventraDbContext context) : base(context)
        {
        }

        public async Task<Stock?> GetByProductAndWarehouseAsync(Guid productId,Guid warehouseId,bool tracking = true,CancellationToken cancellationToken = default)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(x =>x.ProductId == productId &&x.WarehouseId == warehouseId,cancellationToken);
        }
        public async Task<List<GetStocksQueryResponse>>GetStocksAsync(CancellationToken cancellationToken = default)
        {
            return await Table.AsNoTracking().Select(x => new GetStocksQueryResponse
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product.Name,
                    WarehouseId = x.WarehouseId,
                    WarehouseName = x.Warehouse.Name,
                    Quantity = x.Quantity
                })
                .ToListAsync(cancellationToken);
        }
    }
}
