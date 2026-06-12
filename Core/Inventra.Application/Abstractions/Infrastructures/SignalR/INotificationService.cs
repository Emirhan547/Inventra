using Inventra.Application.Features.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Infrastructures.SignalR
{
    public interface INotificationService
    {
        Task SendToAllAsync(NotificationMessage notification);

        Task SendToRoleAsync(string role, NotificationMessage notification);
    }
}
