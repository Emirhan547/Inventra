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
    public sealed class PurchaseOrderApprovedConsumer
    : IConsumer<PurchaseOrderApprovedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseOrderApprovedConsumer(
            INotificationService notificationService,
            INotificationWriteRepository notificationWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _notificationService = notificationService;
            _notificationWriteRepository = notificationWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(
            ConsumeContext<PurchaseOrderApprovedEvent> context)
        {
            var notification = new Notification
            {
                Title = "Satın Alma Talebi Onaylandı",
                Message =
        $"{context.Message.OrderNumber} numaralı talep onaylandı.",
                Type = "PurchaseOrderApproved",

                RoleName = Roles.Employee,

                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            await _notificationWriteRepository
                .AddAsync(notification);

            await _unitOfWork.SaveChangeAsync();

            await _notificationService
                .SendToRoleAsync(
                    Roles.Employee,
                    notification);
        }
    }
}

