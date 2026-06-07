using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.PurchaseOrderRepositories
{
    public class PurchaseOrderReadRepository
        : ReadRepository<PurchaseOrder>,
        IPurchaseOrderReadRepository
    {
        public PurchaseOrderReadRepository(
            InventraDbContext context)
            : base(context)
        {
        }

        public async Task<PurchaseOrder?> GetDetailAsync(
     Guid id,
     CancellationToken cancellationToken = default)
        {
            return await Table
                .Include(x => x.Supplier)
                .Include(x => x.Item)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }
    }
}
