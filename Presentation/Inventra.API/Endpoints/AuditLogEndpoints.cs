using Inventra.Application.Features.AuditLogs.Queries;
using Inventra.Domain.Constants;
using MediatR;

namespace Inventra.API.Endpoints
{
    public static class AuditLogEndpoints
    {
        public static void MapAuditLogEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group =
                app.MapGroup("/auditlogs")
                   .WithTags("AuditLogs");

            group.MapGet(
                "/",
                async (IMediator mediator) =>
                {
                    return await mediator.Send(
                        new GetAuditLogsQuery());
                })
                .RequireAuthorization(
                    Policies.AuditLogView);
        }
    }
}