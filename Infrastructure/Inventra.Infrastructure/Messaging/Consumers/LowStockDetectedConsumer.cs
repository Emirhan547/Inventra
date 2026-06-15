using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Abstractions.Repositories.NotificationRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Contracts.Events;
using Inventra.Application.Features.Notifications;
using Inventra.Domain.Constants;
using Inventra.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Inventra.Infrastructure.Messaging.Consumers
{
    public sealed class LowStockDetectedConsumer
        : IConsumer<LowStockDetectedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LowStockDetectedConsumer> _logger;

        public LowStockDetectedConsumer(
            INotificationService notificationService,
            INotificationWriteRepository notificationWriteRepository,
            IUnitOfWork unitOfWork,
            ILogger<LowStockDetectedConsumer> logger)
        {
            _notificationService = notificationService;
            _notificationWriteRepository = notificationWriteRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Consume(
            ConsumeContext<LowStockDetectedEvent> context)
        {
            var notification = new Notification
            {
                Title = "Kritik Stok Uyarısı",

                Message =
                    $"{context.Message.ProductName} ürünü kritik stok seviyesine düştü. " +
                    $"Kalan stok: {context.Message.CurrentQuantity}",

                Type = "CriticalStock",
                RoleName = Roles.Manager,
                IsRead = false,

                CreatedAt = DateTime.UtcNow
            };

            _logger.LogWarning(
                "LowStockDetectedEvent consumed. ProductName: {ProductName}, CurrentQuantity: {CurrentQuantity}",
                context.Message.ProductName,
                context.Message.CurrentQuantity);

            await _notificationWriteRepository
                .AddAsync(notification);

            await _unitOfWork.SaveChangeAsync();

            await _notificationService
                .SendToRoleAsync(
                    Roles.Manager,
                    notification);

            _logger.LogInformation(
                "Low stock notification sent. ProductName: {ProductName}",
                context.Message.ProductName);
        }
    }
}