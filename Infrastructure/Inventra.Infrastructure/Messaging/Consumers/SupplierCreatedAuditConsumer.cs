using Inventra.Application.Abstractions.Repositories.AuditLogRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Contracts.Events;
using Inventra.Domain.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Messaging.Consumers
{
    public sealed class SupplierCreatedAuditConsumer
    : IConsumer<SupplierCreatedEvent>
    {
        private readonly IAuditLogWriteRepository _auditLogWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SupplierCreatedAuditConsumer(
            IAuditLogWriteRepository auditLogWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _auditLogWriteRepository = auditLogWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(
            ConsumeContext<SupplierCreatedEvent> context)
        {
            await _auditLogWriteRepository.AddAsync(
                new AuditLog
                {
                    EventName = "SupplierCreated",

                    UserId = context.Message.UserId,

                    UserName = context.Message.UserName,

                    Description =
                        $"{context.Message.UserName} kullanıcısı " +
                        $"{context.Message.SupplierName} tedarikçisini oluşturdu.",

                    OccurredOn = DateTime.UtcNow
                });

            await _unitOfWork.SaveChangeAsync();
        }
    }
}
