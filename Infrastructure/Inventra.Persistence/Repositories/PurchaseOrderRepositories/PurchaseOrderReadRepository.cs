using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Application.Features.PurchaseOrders.Results;
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

        public async Task<PurchaseOrderAiDto?> GetAiAnalysisDataAsync(
    Guid id,
    CancellationToken cancellationToken = default)
        {
            return await Table
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new PurchaseOrderAiDto
                {
                    SupplierName = x.Supplier.Name,

                    TotalAmount = x.TotalAmount,

                    Items = x.Item.Select(i => new PurchaseOrderAiItemDto
                    {
                        ProductName = i.Product.Name,

                        Quantity = i.Quantity,

                        UnitPrice = i.UnitPrice,

                        CurrentStock = i.Product.Stocks
                            .Sum(s => (int?)s.Quantity) ?? 0,

                        MinimumStockLevel = i.Product.MinimumStockLevel,

                        Last30DaysMovement = i.Product.Stocks
                            .SelectMany(s => s.StockMovements)
                            .Where(m => m.CreatedDate >= DateTime.UtcNow.AddDays(-30))
                            .Sum(m => (int?)m.Quantity) ?? 0
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);
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