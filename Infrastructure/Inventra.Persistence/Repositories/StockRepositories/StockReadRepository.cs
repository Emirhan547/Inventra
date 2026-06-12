using Inventra.Application.Abstractions.Repositories.StockRepositories;
using Inventra.Application.Common.Pagination;
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
            {
                query = query.AsNoTracking();
            }

            return await query.Include(x => x.Product).Include(x => x.Warehouse).FirstOrDefaultAsync( x => x.ProductId == productId && x.WarehouseId == warehouseId, cancellationToken);
        }

        public async Task<PagedResponse<Stock>>GetPagedStocksAsync(int pageNumber,int pageSize,CancellationToken cancellationToken = default)
        {
            var query = Table.AsNoTracking().Include(x => x.Product).Include(x => x.Warehouse);

            var totalCount =await query.CountAsync(cancellationToken);

            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PagedResponse<Stock>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages =(int)Math.Ceiling(totalCount /(double)pageSize)
            };
        }
    }
}
