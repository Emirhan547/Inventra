using Inventra.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Notifications.Commands
{
    public sealed record MarkNotificationAsReadCommand(
       Guid NotificationId)
       : IRequest<Result>;
}
