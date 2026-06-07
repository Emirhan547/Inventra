using Inventra.Application.Abstractions.Repositories.GenericRepositories;
using Inventra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories
{
    public interface IPurchaseOrderReadRepository
         : IReadRepository<PurchaseOrder>
    {
        Task<PurchaseOrder?> GetDetailAsync(
    Guid id,
    CancellationToken cancellationToken = default);
    }
}
