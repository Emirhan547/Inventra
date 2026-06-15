using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Contracts.Events;
using Inventra.Domain.Entities;
using MassTransit;

namespace Inventra.Infrastructure.Messaging.Consumers;

public sealed class TransferCompletedAuditConsumer
    : IConsumer<TransferCompletedEvent>
{
    private readonly IAuditLogWriteRepository
        _auditLogWriteRepository;

    private readonly IUnitOfWork
        _unitOfWork;

    public TransferCompletedAuditConsumer(
        IAuditLogWriteRepository auditLogWriteRepository,
        IUnitOfWork unitOfWork)
    {
        _auditLogWriteRepository =
            auditLogWriteRepository;

        _unitOfWork =
            unitOfWork;
    }

    public async Task Consume(
        ConsumeContext<TransferCompletedEvent> context)
    {
        await _auditLogWriteRepository.AddAsync(
            new AuditLog
            {
                EventName = "Transfer",

                UserId =
                    context.Message.UserId,

                UserName =
                    context.Message.UserName,

                Description =
                    $"{context.Message.UserName} kullanıcısı " +
                    $"{context.Message.ProductName} ürününden " +
                    $"{context.Message.Quantity} adet transfer gerçekleştirdi.",

                OccurredOn =
                    DateTime.UtcNow
            });

        await _unitOfWork.SaveChangeAsync();
    }
}