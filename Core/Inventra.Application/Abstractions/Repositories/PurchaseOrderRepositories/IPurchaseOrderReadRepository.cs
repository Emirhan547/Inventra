using Inventra.Application.Abstractions.Repositories.GenericRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Domain.Entities;
using Inventra.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories
{
    public interface IPurchaseOrderReadRepository: IReadRepository<PurchaseOrder>
    {
        Task<PurchaseOrder?> GetDetailAsync(Guid id,CancellationToken cancellationToken = default);
        Task<PagedResponse<PurchaseOrder>>GetPagedAsync(int pageNumber,int pageSize,PurchaseOrderStatus? status,Guid? supplierId, CancellationToken cancellationToken = default);
    }
}
