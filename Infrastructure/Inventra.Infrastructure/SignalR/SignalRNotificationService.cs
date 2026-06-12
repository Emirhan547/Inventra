using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Features.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace Inventra.Infrastructure.SignalR
{
    public sealed class SignalRNotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public SignalRNotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendToAllAsync(NotificationMessage notification)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }

        public async Task SendToRoleAsync(string role, NotificationMessage notification)
        {
            await _hubContext.Clients.Group(role).SendAsync("ReceiveNotification", notification);
        }
    }
}