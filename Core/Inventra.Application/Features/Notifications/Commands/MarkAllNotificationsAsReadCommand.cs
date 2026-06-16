using Inventra.Application.Common.Results;
using MediatR;

namespace Inventra.Application.Features.Notifications.Commands
{
    public sealed record MarkAllNotificationsAsReadCommand
        : IRequest<Result>;
}