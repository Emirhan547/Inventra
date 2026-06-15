using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Domain.Entities;
using Inventra.Persistence.Context;
using Inventra.Persistence.Repositories.GenericRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Repositories.AuditLogRepositories
{
    public class AuditLogReadRepository
    : ReadRepository<AuditLog>,
      IAuditLogReadRepository
    {
        public AuditLogReadRepository(
            InventraDbContext context)
            : base(context)
        {
        }
        public async Task<PagedResponse<AuditLog>>
    GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? userName,
        string? eventName,
        DateTime? startDate,
        DateTime? endDate,
        CancellationToken cancellationToken = default)
        {
            IQueryable<AuditLog> query =
                Table.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(userName))
            {
                query = query.Where(x =>
                    x.UserName.Contains(userName));
            }

            if (!string.IsNullOrWhiteSpace(eventName))
            {
                query = query.Where(x =>
                    x.EventName == eventName);
            }

            if (startDate.HasValue)
            {
                query = query.Where(x =>
                    x.OccurredOn >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(x =>
                    x.OccurredOn <= endDate.Value);
            }

            query =
                query.OrderByDescending(
                    x => x.OccurredOn);

            var totalCount =
                await query.CountAsync(
                    cancellationToken);

            var items =
                await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

            return new PagedResponse<AuditLog>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages =
                    (int)Math.Ceiling(
                        totalCount /
                        (double)pageSize)
            };
        }
    }
}
