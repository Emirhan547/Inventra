using Inventra.Application.Common.Pagination;
using Inventra.Application.Features.AuditLogs.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.AuditLogs.Queries
{
    public sealed class GetAuditLogsQuery: IRequest<PagedResponse<GetAuditLogsResponse>>
    {
        public string? UserName { get; set; }

        public string? EventName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
