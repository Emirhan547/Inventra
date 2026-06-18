using Inventra.Application.Features.AuditLogs.Queries;
using Inventra.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventra.API.Endpoints
{
    public static class AuditLogEndpoints
    {
        public static void MapAuditLogEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group =app.MapGroup("/auditlogs").WithTags("AuditLogs");

            group.MapGet( "/",async (string? userName,string? eventName,DateTime? startDate,DateTime? endDate,int? pageNumber,int? pageSize,IMediator mediator) =>
     {
         var query = new GetAuditLogsQuery
         {
             UserName = userName,
             EventName = eventName,
             StartDate = startDate,
             EndDate = endDate,
             PageNumber = pageNumber ?? 1,
             PageSize = pageSize ?? 10
         };

         return await mediator.Send(query);
     }).RequireAuthorization(Policies.AuditLogView);
        }
    }
}