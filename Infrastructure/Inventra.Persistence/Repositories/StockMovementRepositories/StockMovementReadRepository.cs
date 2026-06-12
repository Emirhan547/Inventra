using Inventra.Application.Abstractions.Repositories.StockMovementRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Application.Features.StockMovements.Results;
using Inventra.Domain.Entities;
using Inventra.Domain.Enums;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.StockMovementRepositories
{
    public class StockMovementReadRepository : ReadRepository<StockMovement>, IStockMovementReadRepository
    {
        public StockMovementReadRepository(InventraDbContext context) : base(context)
        {
        }
        public async Task<PagedResponse<StockMovement>>GetPagedStockMovementsAsync(int pageNumber,int pageSize,StockMovementType? type,DateTime? startDate,DateTime? endDate,
         CancellationToken cancellationToken = default)
        {
            IQueryable<StockMovement> query = Table.AsNoTracking().Include(x => x.Stock).ThenInclude(x => x.Product).Include(x => x.Stock).ThenInclude(x => x.Warehouse);

            if (type.HasValue)
            {
                query = query.Where(x =>x.Type == type.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(x =>x.CreatedDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(x =>x.CreatedDate <= endDate.Value);
            }

            query = query.OrderByDescending(x => x.CreatedDate);

            var totalCount =await query.CountAsync(cancellationToken);

            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PagedResponse<StockMovement>
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
