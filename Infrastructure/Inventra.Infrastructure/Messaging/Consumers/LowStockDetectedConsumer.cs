using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Contracts.Events;
using Inventra.Application.Features.Notifications;
using Inventra.Domain.Constants;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Messaging.Consumers
{
    public sealed class LowStockDetectedConsumer : IConsumer<LowStockDetectedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<LowStockDetectedConsumer> _logger;
        public LowStockDetectedConsumer(INotificationService notificationService, ILogger<LowStockDetectedConsumer> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<LowStockDetectedEvent> context)
        {
            var notification = new NotificationMessage
            {
                Title = "Kritik Stok Uyarısı",

                Message =$"{context.Message.ProductName} ürünü kritik stok seviyesine düştü. " + $"Kalan stok: {context.Message.CurrentQuantity}",

                Type = "CriticalStock",

                CreatedAt = DateTime.UtcNow
            };

            _logger.LogWarning(
     "LowStockDetectedEvent consumed. ProductName: {ProductName}, CurrentQuantity: {CurrentQuantity}",
     context.Message.ProductName,
     context.Message.CurrentQuantity);

            await _notificationService.SendToRoleAsync(
                Roles.Manager,
                notification);

            _logger.LogInformation(
                "Low stock notification sent. ProductName: {ProductName}",
                context.Message.ProductName);
        }
    }
}