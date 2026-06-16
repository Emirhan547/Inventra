using Inventra.Domain.Constants;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Inventra.API.Endpoints
{
    public static  class HealthEndpoints
    {
        public static void MapHealthEndpoints(
            this IEndpointRouteBuilder app)
        {
            app.MapHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        ResponseWriter = async (
            context,
            report) =>
        {
            
        }
    })
    .RequireAuthorization(
        Policies.AuditLogView);
        }
    }
}
