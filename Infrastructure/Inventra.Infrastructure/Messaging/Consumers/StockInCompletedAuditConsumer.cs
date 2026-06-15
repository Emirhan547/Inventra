using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Contracts.Events;
using Inventra.Domain.Entities;
using MassTransit;

namespace Inventra.Infrastructure.Messaging.Consumers;

public sealed class StockInCompletedAuditConsumer
    : IConsumer<StockInCompletedEvent>
{
    private readonly IAuditLogWriteRepository
        _auditLogWriteRepository;

    private readonly IUnitOfWork
        _unitOfWork;

    public StockInCompletedAuditConsumer(
        IAuditLogWriteRepository auditLogWriteRepository,
        IUnitOfWork unitOfWork)
    {
        _auditLogWriteRepository =
            auditLogWriteRepository;

        _unitOfWork =
            unitOfWork;
    }

    public async Task Consume(
        ConsumeContext<StockInCompletedEvent> context)
    {
        await _auditLogWriteRepository.AddAsync(
            new AuditLog
            {
                EventName = "StockIn",

                UserId =
                    context.Message.UserId,

                UserName =
                    context.Message.UserName,

                Description =
                    $"{context.Message.UserName} kullanıcısı " +
                    $"{context.Message.ProductName} ürününe " +
                    $"{context.Message.Quantity} adet stok girişi yaptı.",

                OccurredOn =
                    DateTime.UtcNow
            });

        await _unitOfWork.SaveChangeAsync();
    }
}