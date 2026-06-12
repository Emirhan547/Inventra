using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Contracts.Events;
using Inventra.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Inventra.Infrastructure.Messaging.Consumers;

public sealed class PurchaseOrderApprovedAuditConsumer
    : IConsumer<PurchaseOrderApprovedEvent>
{
    private readonly IAuditLogWriteRepository _auditLogWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PurchaseOrderApprovedAuditConsumer> _logger;
    public PurchaseOrderApprovedAuditConsumer(
        IAuditLogWriteRepository auditLogWriteRepository,
        IUnitOfWork unitOfWork,
        ILogger<PurchaseOrderApprovedAuditConsumer> logger)
    {
        _auditLogWriteRepository = auditLogWriteRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Consume(
        ConsumeContext<PurchaseOrderApprovedEvent> context)
    {
        var auditLog = new AuditLog
        {
            EventName = "PurchaseOrderApproved",

            UserId = context.Message.UserId,

            UserName = context.Message.UserName,

            Description =
                $"{context.Message.UserName} kullanıcısı " +
                $"{context.Message.OrderNumber} numaralı satın alma talebini onayladı.",

            OccurredOn = DateTime.UtcNow
        };

        await _auditLogWriteRepository.AddAsync(auditLog);

        await _unitOfWork.SaveChangeAsync();
        _logger.LogInformation(
    "Audit log created. EventName: {EventName}, UserName: {UserName}",
    auditLog.EventName,
    auditLog.UserName);
    }
}