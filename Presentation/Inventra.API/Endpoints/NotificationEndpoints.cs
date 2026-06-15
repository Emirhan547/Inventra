using Inventra.Application.Features.Notifications.Queries;
using MediatR;

namespace Inventra.API.Endpoints
{
    public static class NotificationEndpoints
    {
        public static void MapNotificationEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group =
                app.MapGroup("/notifications")
                    .WithTags("Notifications");

            group.MapGet(
                "/",
                async (IMediator mediator) =>
                    await mediator.Send(
                        new GetNotificationsQuery()));
        }
    }
}