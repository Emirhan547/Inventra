using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Messaging;
using Inventra.Application.Abstractions.Repositories.SupplierRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Contracts.Events;
using Inventra.Application.Features.Suppliers.Commands;
using Inventra.Domain.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Suppliers.Handlers
{
    public class CreateSupplierCommandHandler(ISupplierReadRepository _supplierReadRepository,ISupplierWriteRepository _supplierWriteRepository,IUnitOfWork _unitOfWork, IEventBus _eventBus,
ICurrentUserService _currentUserService) : IRequestHandler<CreateSupplierCommandRequest, Result<CreateSupplierCommandResponse>>
    {
        public async Task<Result<CreateSupplierCommandResponse>> Handle(CreateSupplierCommandRequest request, CancellationToken cancellationToken)
        {
             var email= await _supplierReadRepository.AnyAsync(x=>x.Email == request.Email);
            if(email)
            {
                return Result<CreateSupplierCommandResponse>.Failure("Supplier email already exists.");
            }
            var mapped = request.Adapt<Supplier>();
            await _supplierWriteRepository.AddAsync(mapped);
            await _unitOfWork.SaveChangeAsync();
            await _eventBus.PublishAsync(
    new SupplierCreatedEvent
    {
        SupplierId = mapped.Id,
        SupplierName = mapped.Name,

        UserId = _currentUserService.UserId,
        UserName = _currentUserService.UserName
    });
            return Result<CreateSupplierCommandResponse>.SuccessResult(new CreateSupplierCommandResponse
            {
                Id = mapped.Id
            },
                "Supplier created successfully.");
        }
    }
}
