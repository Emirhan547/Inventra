using Inventra.Application.Features.Notifications.Commands;
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
            group.MapPut(
    "/{id:guid}/read",
    async (
        Guid id,
        IMediator mediator) =>
    {
        return await mediator.Send(
            new MarkNotificationAsReadCommand(id));
    });
            group.MapPut(
    "/read-all",
    async (IMediator mediator) =>
    {
        return await mediator.Send(
            new MarkAllNotificationsAsReadCommand());
    });
            group.MapGet(
    "/unread-count",
    async (IMediator mediator) =>
    {
        return await mediator.Send(
            new GetUnreadNotificationCountQuery());
    });
        }

    }
}