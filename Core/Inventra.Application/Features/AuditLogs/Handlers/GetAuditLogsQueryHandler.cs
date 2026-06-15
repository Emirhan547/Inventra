using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Common.Pagination;
using Inventra.Application.Features.AuditLogs.Queries;
using Inventra.Application.Features.AuditLogs.Results;
using MediatR;

namespace Inventra.Application.Features.AuditLogs.Handlers
{
    public sealed class GetAuditLogsQueryHandler
        : IRequestHandler<
            GetAuditLogsQuery,
            PagedResponse<GetAuditLogsResponse>>
    {
        private readonly IAuditLogReadRepository
            _auditLogReadRepository;

        public GetAuditLogsQueryHandler(
            IAuditLogReadRepository auditLogReadRepository)
        {
            _auditLogReadRepository =
                auditLogReadRepository;
        }

        public async Task<
            PagedResponse<GetAuditLogsResponse>>
            Handle(
                GetAuditLogsQuery request,
                CancellationToken cancellationToken)
        {
            var pagedLogs =
                await _auditLogReadRepository
                    .GetPagedAsync(
                        request.PageNumber,
                        request.PageSize,
                        request.UserName,
                        request.EventName,
                        request.StartDate,
                        request.EndDate,
                        cancellationToken);

            return new PagedResponse<
                GetAuditLogsResponse>
            {
                Items = pagedLogs.Items
                    .Select(x =>
                        new GetAuditLogsResponse
                        {
                            Id = x.Id,
                            EventName = x.EventName,
                            UserName = x.UserName,
                            Description = x.Description,
                            OccurredOn = x.OccurredOn
                        })
                    .ToList(),

                PageNumber =
                    pagedLogs.PageNumber,

                PageSize =
                    pagedLogs.PageSize,

                TotalCount =
                    pagedLogs.TotalCount,

                TotalPages =
                    pagedLogs.TotalPages
            };
        }
    }
}