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
    public sealed class PurchaseOrderCompletedConsumer
      : IConsumer<PurchaseOrderCompletedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PurchaseOrderCompletedConsumer(
            INotificationService notificationService, IUnitOfWork unitOfWork, INotificationWriteRepository notificationWriteRepository)
        {
            _notificationService = notificationService;
            _unitOfWork = unitOfWork;
            _notificationWriteRepository = notificationWriteRepository;
        }

        public async Task Consume(
            ConsumeContext<PurchaseOrderCompletedEvent> context)
        {
            var notification = new Notification
            {
                Title = "Satın Alma Süreci Tamamlandı",
                Message =
         $"{context.Message.OrderNumber} numaralı sipariş tamamlandı.",
                Type = "PurchaseOrderCompleted",

                RoleName = Roles.Employee,

                IsRead = false,

                CreatedAt = DateTime.UtcNow
            };

            await _notificationWriteRepository.AddAsync(notification);
            await _unitOfWork.SaveChangeAsync();

            await _notificationService.SendToRoleAsync(
                Roles.Employee,
                notification);
        }
    }
}
