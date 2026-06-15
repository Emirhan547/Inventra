using Inventra.Application.Features.Notifications;
using Inventra.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Infrastructures.SignalR
{
    public interface INotificationService
    {
        Task SendToAllAsync(Notification notification);

        Task SendToRoleAsync(string role, Notification notification);
    }
}
