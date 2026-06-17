using Inventra.Application.Abstractions.Repositories.SupplierRepositories;
using Inventra.Application.Features.Suppliers.Results;
using Inventra.Domain.Entities;
using Inventra.Domain.Enums;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.SupplierRepositories
{
    public class SupplierReadRepository : ReadRepository<Supplier>, ISupplierReadRepository
    {
        public SupplierReadRepository(InventraDbContext context) : base(context)
        {
        }

        public async Task<GetSupplierByIdQueryResponse?>GetSupplierByIdAsync(Guid id,CancellationToken cancellationToken = default)
        {
            return await Table.AsNoTracking().Where(x => x.Id == id).Select(x => new GetSupplierByIdQueryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Email = x.Email
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<GetSuppliersQueryResponse>>GetSuppliersAsync(CancellationToken cancellationToken = default)
        {
            return await Table.AsNoTracking().Select(x => new GetSuppliersQueryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Email = x.Email
                })
                .ToListAsync(cancellationToken);
        }
        public async Task<SupplierAiAnalysisData>
    GetSupplierAiAnalysisDataAsync(
        Guid supplierId,
        CancellationToken cancellationToken = default)
        {
            var supplier =
                await _context.Suppliers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        x => x.Id == supplierId,
                        cancellationToken);

            if (supplier is null)
            {
                throw new Exception("Supplier not found.");
            }

            var orders =
                await _context.PurchaseOrders
                    .AsNoTracking()
                    .Where(x => x.SupplierId == supplierId)
                    .ToListAsync(cancellationToken);

            return new SupplierAiAnalysisData
            {
                SupplierName =
                    supplier.Name,

                TotalOrders =
                    orders.Count,

                CompletedOrders =
                    orders.Count(x =>
                        x.Status ==
                        PurchaseOrderStatus.Completed),

                PendingOrders =
                    orders.Count(x =>
                        x.Status ==
                        PurchaseOrderStatus.Pending),

                CancelledOrders =
                    orders.Count(x =>
                        x.Status ==
                        PurchaseOrderStatus.Cancelled),

                TotalAmount =
                    orders.Sum(x =>
                        x.TotalAmount),

                AverageOrderAmount =
                    orders.Any()
                        ? orders.Average(x =>
                            x.TotalAmount)
                        : 0,

                LastOrderDate =
                    orders
                        .OrderByDescending(x =>
                            x.CreatedDate)
                        .Select(x =>
                            (DateTime?)x.CreatedDate)
                        .FirstOrDefault()
            };
        }
    }
}
