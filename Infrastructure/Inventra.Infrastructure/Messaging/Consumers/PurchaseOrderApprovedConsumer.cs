using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Contracts.Events;
using Inventra.Application.Features.Notifications;
using Inventra.Domain.Constants;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Messaging.Consumers
{
    public sealed class PurchaseOrderApprovedConsumer
     : IConsumer<PurchaseOrderApprovedEvent>
    {
        private readonly INotificationService _notificationService;

        public PurchaseOrderApprovedConsumer(
            INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Consume(
            ConsumeContext<PurchaseOrderApprovedEvent> context)
        {
            var notification = new NotificationMessage
            {
                Title = "Satın Alma Talebi Onaylandı",
                Message =
                    $"{context.Message.OrderNumber} numaralı talep onaylandı.",
                Type = "PurchaseOrderApproved",
                CreatedAt = DateTime.UtcNow
            };

            await _notificationService.SendToRoleAsync(
                Roles.Employee,
                notification);
        }
    }
}
