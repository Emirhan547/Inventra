using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Contracts.Events;
using Inventra.Domain.Entities;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Inventra.Infrastructure.Messaging.Consumers;

public sealed class RoleAssignedAuditConsumer
    : IConsumer<RoleAssignedEvent>
{
    private readonly IAuditLogWriteRepository _auditLogWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RoleAssignedAuditConsumer> _logger;
    public RoleAssignedAuditConsumer(
        IAuditLogWriteRepository auditLogWriteRepository,
        IUnitOfWork unitOfWork,
        ILogger<RoleAssignedAuditConsumer> logger)
    {
        _auditLogWriteRepository = auditLogWriteRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Consume(
     ConsumeContext<RoleAssignedEvent> context)
    {
        _logger.LogInformation(
            "RoleAssignedEvent consumed. UserName: {UserName}, RoleName: {RoleName}",
            context.Message.UserName,
            context.Message.RoleName);

        var auditLog = new AuditLog
        {
            EventName = "RoleAssigned",

            UserId = context.Message.AssignedByUserId,

            UserName =
                context.Message.AssignedByUserName,

            Description =
                $"{context.Message.AssignedByUserName} kullanıcısı " +
                $"{context.Message.UserName} kullanıcısına " +
                $"{context.Message.RoleName} rolü atadı.",

            OccurredOn = DateTime.UtcNow
        };

        await _auditLogWriteRepository.AddAsync(auditLog);

        await _unitOfWork.SaveChangeAsync();

        _logger.LogInformation(
            "Audit log created. EventName: {EventName}, AssignedBy: {AssignedBy}",
            auditLog.EventName,
            auditLog.UserName);
    }
}