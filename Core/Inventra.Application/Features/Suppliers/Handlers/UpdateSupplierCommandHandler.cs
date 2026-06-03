using Inventra.Application.Abstractions.Repositories.SupplierRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Suppliers.Commands;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Suppliers.Handlers
{
    public class UpdateSupplierCommandHandler(ISupplierReadRepository _supplierReadRepository,ISupplierWriteRepository _supplierWriteRepository,IUnitOfWork _unitOfWork) : IRequestHandler<UpdateSupplierCommand, Result>
    {
        public async Task<Result> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var email=await _supplierReadRepository.AnyAsync(x=> x.Email == request.Email);
            if(email)
            {
                return Result.Failure("Supplier email already exists.");
            }
            var supplier = await _supplierReadRepository.GetByIdAsync(request.Id,tracking:true,cancellationToken);
            if(supplier==null)
            {
                return Result.Failure("Supplier Not Found");
            }
            supplier=request.Adapt(supplier);
            _supplierWriteRepository.Update(supplier);
            await _unitOfWork.SaveChangeAsync();
            return Result.SuccessResult("Supplier Successfully Update");
        }
    }
}
