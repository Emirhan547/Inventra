using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Contracts.Events;
using Inventra.Application.Features.Notifications;
using Inventra.Domain.Constants;
using Inventra.Domain.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Messaging.Consumers
{
    public sealed class PurchaseOrderCompletedConsumer
      : IConsumer<PurchaseOrderCompletedEvent>
    {
        private readonly INotificationService _notificationService;

        public PurchaseOrderCompletedConsumer(
            INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Consume(
            ConsumeContext<PurchaseOrderCompletedEvent> context)
        {
            await _notificationService.SendToRoleAsync(
                Roles.Employee,
                new Notification
                {
                    Title = "Satın Alma Süreci Tamamlandı",
                    Message =
                        $"{context.Message.OrderNumber} numaralı sipariş tamamlandı.",
                    Type = "PurchaseOrderCompleted",
                    CreatedAt = DateTime.UtcNow
                });
        }
    }
}
