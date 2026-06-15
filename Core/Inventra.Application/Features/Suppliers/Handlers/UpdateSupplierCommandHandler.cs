using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Messaging;
using Inventra.Application.Abstractions.Repositories.SupplierRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Contracts.Events;
using Inventra.Application.Features.Suppliers.Commands;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Suppliers.Handlers
{
    public class UpdateSupplierCommandHandler(ISupplierReadRepository _supplierReadRepository,ISupplierWriteRepository _supplierWriteRepository,IUnitOfWork _unitOfWork, IEventBus _eventBus,
ICurrentUserService _currentUserService) : IRequestHandler<UpdateSupplierCommand, Result>
    {
        public async Task<Result> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _supplierReadRepository.AnyAsync(
     x => x.Email == request.Email
       && x.Id != request.Id);
            if (emailExists)
            {
                return Result.Failure(
                    "Supplier email already exists.");
            }
            var supplier = await _supplierReadRepository.GetByIdAsync(request.Id,tracking:true,cancellationToken);
            if(supplier==null)
            {
                return Result.Failure("Supplier Not Found");
            }
            supplier=request.Adapt(supplier);
            _supplierWriteRepository.Update(supplier);
            await _unitOfWork.SaveChangeAsync();
            await _eventBus.PublishAsync(
    new SupplierUpdatedEvent
    {
        SupplierId = supplier.Id,
        SupplierName = supplier.Name,

        UserId = _currentUserService.UserId,
        UserName = _currentUserService.UserName
    });
            return Result.SuccessResult("Supplier Successfully Update");
        }
    }
}
