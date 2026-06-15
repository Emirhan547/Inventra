using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Contracts.Events;
using Inventra.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Inventra.Infrastructure.Messaging.Consumers;

public sealed class ProductUpdatedAuditConsumer
    : IConsumer<ProductUpdatedEvent>
{
    private readonly IAuditLogWriteRepository _auditLogWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductUpdatedAuditConsumer> _logger;

    public ProductUpdatedAuditConsumer(
        IAuditLogWriteRepository auditLogWriteRepository,
        IUnitOfWork unitOfWork,
        ILogger<ProductUpdatedAuditConsumer> logger)
    {
        _auditLogWriteRepository = auditLogWriteRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Consume(
        ConsumeContext<ProductUpdatedEvent> context)
    {
        var auditLog = new AuditLog
        {
            EventName = "ProductUpdated",

            UserId = context.Message.UserId,

            UserName = context.Message.UserName,

            Description =
                $"{context.Message.UserName} kullanıcısı " +
                $"{context.Message.ProductName} ürününü güncelledi.",

            OccurredOn = DateTime.UtcNow
        };

        await _auditLogWriteRepository.AddAsync(auditLog);

        await _unitOfWork.SaveChangeAsync();

        _logger.LogInformation(
            "ProductUpdated audit log created. Product: {Product}",
            context.Message.ProductName);
    }
}