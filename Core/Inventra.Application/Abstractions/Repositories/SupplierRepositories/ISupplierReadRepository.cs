using Inventra.Application.Abstractions.Repositories.GenericRepositories;
using Inventra.Application.Features.Suppliers.Results;
using Inventra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.SupplierRepositories
{
    public interface ISupplierReadRepository:IReadRepository<Supplier>
    {
        Task<List<GetSuppliersQueryResponse>>GetSuppliersAsync(CancellationToken cancellationToken = default);
        Task<GetSupplierByIdQueryResponse?>GetSupplierByIdAsync(Guid id,CancellationToken cancellationToken = default);
    }
}
