using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Contracts.Events;
using Inventra.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Inventra.Infrastructure.Messaging.Consumers;

public sealed class ProductCreatedAuditConsumer
    : IConsumer<ProductCreatedEvent>
{
    private readonly IAuditLogWriteRepository _auditLogWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductCreatedAuditConsumer> _logger;

    public ProductCreatedAuditConsumer(
        IAuditLogWriteRepository auditLogWriteRepository,
        IUnitOfWork unitOfWork,
        ILogger<ProductCreatedAuditConsumer> logger)
    {
        _auditLogWriteRepository = auditLogWriteRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Consume(
        ConsumeContext<ProductCreatedEvent> context)
    {
        var auditLog = new AuditLog
        {
            EventName = "ProductCreated",

            UserId = context.Message.UserId,

            UserName = context.Message.UserName,

            Description =
                $"{context.Message.UserName} kullanıcısı " +
                $"{context.Message.ProductName} ürününü oluşturdu.",

            OccurredOn = DateTime.UtcNow
        };

        await _auditLogWriteRepository.AddAsync(auditLog);

        await _unitOfWork.SaveChangeAsync();

        _logger.LogInformation(
            "ProductCreated audit log created. Product: {Product}",
            context.Message.ProductName);
    }
}