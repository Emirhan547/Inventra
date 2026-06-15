using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Contracts.Events;
using Inventra.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Inventra.Infrastructure.Messaging.Consumers;

public sealed class ProductDeletedAuditConsumer
    : IConsumer<ProductDeletedEvent>
{
    private readonly IAuditLogWriteRepository _auditLogWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductDeletedAuditConsumer> _logger;

    public ProductDeletedAuditConsumer(
        IAuditLogWriteRepository auditLogWriteRepository,
        IUnitOfWork unitOfWork,
        ILogger<ProductDeletedAuditConsumer> logger)
    {
        _auditLogWriteRepository = auditLogWriteRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Consume(
        ConsumeContext<ProductDeletedEvent> context)
    {
        var auditLog = new AuditLog
        {
            EventName = "ProductDeleted",

            UserId = context.Message.UserId,

            UserName = context.Message.UserName,

            Description =
                $"{context.Message.UserName} kullanıcısı " +
                $"{context.Message.ProductName} ürününü sildi.",

            OccurredOn = DateTime.UtcNow
        };

        await _auditLogWriteRepository.AddAsync(auditLog);

        await _unitOfWork.SaveChangeAsync();

        _logger.LogInformation(
            "ProductDeleted audit log created. Product: {Product}",
            context.Message.ProductName);
    }
}