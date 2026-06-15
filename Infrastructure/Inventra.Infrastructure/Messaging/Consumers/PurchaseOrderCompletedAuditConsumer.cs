using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Contracts.Events;
using Inventra.Domain.Entities;
using MassTransit;

namespace Inventra.Infrastructure.Messaging.Consumers;

public sealed class PurchaseOrderCompletedAuditConsumer
    : IConsumer<PurchaseOrderCompletedEvent>
{
    private readonly IAuditLogWriteRepository
        _auditLogWriteRepository;

    private readonly IUnitOfWork _unitOfWork;

    public PurchaseOrderCompletedAuditConsumer(
        IAuditLogWriteRepository auditLogWriteRepository,
        IUnitOfWork unitOfWork)
    {
        _auditLogWriteRepository =
            auditLogWriteRepository;

        _unitOfWork =
            unitOfWork;
    }

    public async Task Consume(
        ConsumeContext<PurchaseOrderCompletedEvent> context)
    {
        await _auditLogWriteRepository.AddAsync(
            new AuditLog
            {
                EventName =
                    "PurchaseOrderCompleted",

                UserId =
                    context.Message.UserId,

                UserName =
                    context.Message.UserName,

                Description =
                    $"{context.Message.UserName} kullanıcısı " +
                    $"{context.Message.OrderNumber} numaralı satın alma siparişini tamamladı.",

                OccurredOn =
                    DateTime.UtcNow
            });

        await _unitOfWork.SaveChangeAsync();
    }
}