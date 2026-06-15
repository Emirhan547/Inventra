using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Features.AuditLogs.Queries;
using Inventra.Application.Features.AuditLogs.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.AuditLogs.Handlers
{
    public sealed class GetAuditLogsQueryHandler
    : IRequestHandler<
        GetAuditLogsQuery,
        List<GetAuditLogsResponse>>
    {
        private readonly IAuditLogReadRepository
            _auditLogReadRepository;

        public GetAuditLogsQueryHandler(
            IAuditLogReadRepository auditLogReadRepository)
        {
            _auditLogReadRepository =
                auditLogReadRepository;
        }

        public async Task<List<GetAuditLogsResponse>>
            Handle(
                GetAuditLogsQuery request,
                CancellationToken cancellationToken)
        {
            var auditLogs =
     await _auditLogReadRepository
         .GetAllAsync(false);

            return auditLogs
                .OrderByDescending(x => x.OccurredOn)
                .Select(x =>
                    new GetAuditLogsResponse
                    {
                        Id = x.Id,
                        EventName = x.EventName,
                        UserName = x.UserName,
                        Description = x.Description,
                        OccurredOn = x.OccurredOn
                    })
                .ToList();
        }
    }
}