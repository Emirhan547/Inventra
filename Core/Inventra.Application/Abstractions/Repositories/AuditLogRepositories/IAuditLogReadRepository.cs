using Inventra.Application.Abstractions.Repositories.GenericRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Repositories.AuditLogRepositories
{
    public interface IAuditLogReadRepository
    : IReadRepository<AuditLog>
    {
        Task<PagedResponse<AuditLog>>GetPagedAsync(int pageNumber,int pageSize, string? userName,string? eventName,DateTime? startDate,DateTime? endDate,CancellationToken cancellationToken = default);
    }
}
