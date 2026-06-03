using Inventra.Application.Abstractions.Repositories.SupplierRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Suppliers.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Suppliers.Handlers
{
    public class RemoveSupplierCommandHandler(ISupplierReadRepository _supplierReadRepository,ISupplierWriteRepository _supplierWriteRepository,IUnitOfWork _unitOfWork) : IRequestHandler<RemoveSupplierCommand, Result>
    {
        public async Task<Result> Handle(RemoveSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierReadRepository.GetByIdAsync(request.Id, tracking: true, cancellationToken);
            if (supplier == null)
            {
                return Result.Failure("Supplier Not Found");
            }
            _supplierWriteRepository.Remove(supplier);
            await _unitOfWork.SaveChangeAsync();
            return Result.SuccessResult("Supplier Success Deleted");
        }
    }
}
