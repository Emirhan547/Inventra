using Inventra.Application.Contracts.Events;
using Inventra.Infrastructure.SignalR;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Messaging.Consumers
{
    public sealed class DashboardUpdatedConsumer
    : IConsumer<DashboardUpdatedEvent>
    {
        private readonly IHubContext<NotificationHub>
            _hubContext;

        public DashboardUpdatedConsumer(
            IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Consume(
            ConsumeContext<DashboardUpdatedEvent> context)
        {
            await _hubContext
                .Clients
                .All
                .SendAsync("DashboardUpdated");
        }
    }
}