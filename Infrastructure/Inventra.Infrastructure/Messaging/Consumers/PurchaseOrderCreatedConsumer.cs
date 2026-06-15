using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Abstractions.Repositories.NotificationRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Contracts.Events;
using Inventra.Application.Features.Notifications;
using Inventra.Domain.Constants;
using Inventra.Domain.Entities;
using MassTransit;

namespace Inventra.Infrastructure.Messaging.Consumers
{
    public sealed class PurchaseOrderCreatedConsumer
        : IConsumer<PurchaseOrderCreatedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationWriteRepository _notificationWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseOrderCreatedConsumer(
            INotificationService notificationService,
            INotificationWriteRepository notificationWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _notificationService = notificationService;
            _notificationWriteRepository = notificationWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(
            ConsumeContext<PurchaseOrderCreatedEvent> context)
        {
            var notification = new Notification
            {
                Title = "Yeni Satın Alma Talebi",
                Message =
        $"{context.Message.OrderNumber} numaralı talep oluşturuldu.",
                Type = "PurchaseOrderCreated",

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