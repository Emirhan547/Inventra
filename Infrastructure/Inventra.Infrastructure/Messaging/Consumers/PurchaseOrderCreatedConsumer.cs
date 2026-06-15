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
    public sealed class PurchaseOrderCreatedConsumer
    : IConsumer<PurchaseOrderCreatedEvent>
    {
        private readonly INotificationService _notificationService;

        public PurchaseOrderCreatedConsumer(
            INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Consume(
            ConsumeContext<PurchaseOrderCreatedEvent> context)
        {
            await _notificationService.SendToRoleAsync(
                Roles.Manager,
                new Notification
                {
                    Title = "Yeni Satın Alma Talebi",
                    Message =
                        $"{context.Message.OrderNumber} numaralı talep oluşturuldu.",
                    Type = "PurchaseOrderCreated",
                    CreatedAt = DateTime.UtcNow
                });
        }
    }
}
