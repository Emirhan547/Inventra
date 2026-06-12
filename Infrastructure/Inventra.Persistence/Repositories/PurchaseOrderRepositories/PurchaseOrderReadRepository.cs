using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Domain.Entities;
using Inventra.Domain.Enums;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.PurchaseOrderRepositories
{
    public class PurchaseOrderReadRepository : ReadRepository<PurchaseOrder>, IPurchaseOrderReadRepository
    {
        public PurchaseOrderReadRepository(InventraDbContext context) : base(context)
        {
        }

        public async Task<PurchaseOrder?> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await Table.Include(x => x.Supplier).Include(x => x.Item).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<PagedResponse<PurchaseOrder>>GetPagedAsync(int pageNumber,int pageSize,PurchaseOrderStatus? status,Guid? supplierId,CancellationToken cancellationToken = default)
        {
            IQueryable<PurchaseOrder> query = Table.AsNoTracking().Include(x => x.Supplier).Include(x => x.Item).ThenInclude(x => x.Product);

            if (status.HasValue)
            {
                query = query.Where(x =>x.Status == status.Value);
            }

            if (supplierId.HasValue)
            {
                query = query.Where(x =>x.SupplierId == supplierId.Value);
            }
            var totalCount =await query.CountAsync(cancellationToken);
            var items = await query.OrderByDescending(x => x.CreatedDate).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PagedResponse<PurchaseOrder>
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