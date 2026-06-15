using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Features.Notifications;
using Inventra.Domain.Entities;
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

        public async Task SendToAllAsync(Notification notification)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification);
        }

        public async Task SendToRoleAsync(
    string role,
    Notification notification)
        {
            Console.WriteLine(
                $"SIGNALR GONDERILIYOR => {role}");

            await _hubContext
                .Clients
                .Group(role)
                .SendAsync(
                    "ReceiveNotification",
                    notification);

            Console.WriteLine(
                $"SIGNALR GONDERILDI => {role}");
        }
    }
}