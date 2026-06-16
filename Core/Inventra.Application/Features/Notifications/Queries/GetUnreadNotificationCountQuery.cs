using MediatR;

namespace Inventra.Application.Features.Notifications.Queries
{
    public sealed record
        GetUnreadNotificationCountQuery
        : IRequest<int>;
}