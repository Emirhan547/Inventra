using Inventra.Application.Abstractions.Repositories.SupplierRepositories;
using Inventra.Application.Features.Suppliers.Results;
using Inventra.Domain.Entities;
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
    }
}
