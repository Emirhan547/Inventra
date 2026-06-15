using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Abstractions.Repositories.NotificationRepositories;
using Inventra.Application.Abstractions.Uow;
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
    public sealed class StockInCompletedConsumer
    : IConsumer<StockInCompletedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StockInCompletedConsumer(
            INotificationService notificationService,
            INotificationWriteRepository notificationWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _notificationService = notificationService;
            _notificationWriteRepository = notificationWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(
            ConsumeContext<StockInCompletedEvent> context)
        {
            var notification = new Notification
            {
                Title = "Stok Girişi",
                Message =
                    $"{context.Message.ProductName} ürününe {context.Message.Quantity} adet giriş yapıldı.",
                Type = "StockIn",
                RoleName = Roles.Manager,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await _notificationWriteRepository
                .AddAsync(notification);

            await _unitOfWork.SaveChangeAsync();

            await _notificationService
                .SendToRoleAsync(
                    Roles.Manager,
                    notification);
        }
    }
}

