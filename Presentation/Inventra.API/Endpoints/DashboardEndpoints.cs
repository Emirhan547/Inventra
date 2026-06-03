using Inventra.Application.Features.Dashboards.Queries;
using MediatR;

namespace Inventra.API.Endpoints
{
    public static class DashboardEndpoints
    {
        public static void MapDashboardEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group =
                app.MapGroup("/dashboard")
                   .WithTags("Dashboard");

            group.MapGet("/",
                async (
                    IMediator mediator)
                    => await mediator.Send(
                        new GetDashboardQueryRequest()));
        }
    }
}
