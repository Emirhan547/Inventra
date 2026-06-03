using Inventra.Application.Abstractions.Repositories.WarehouseRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Warehouses.Commands;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Warehouses.Handlers
{
    public class UpdateWarehouseCommandHandler(IWarehouseReadRepository _warehouseReadRepository,IWarehouseWriteRepository _warehouseWriteRepository,IUnitOfWork _unitOfWork) : IRequestHandler<UpdateWarehouseCommand, Result>
    {
        public async Task<Result> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var result=await _warehouseReadRepository.GetByIdAsync(request.Id);
            if (result == null)
            {
                return Result.Failure("Warehouse Not Found");
            }
            result=request.Adapt(result);
            _warehouseWriteRepository.Update(result);
            await _unitOfWork.SaveChangeAsync();
            return Result.SuccessResult("Warehouse Success Added");
        }
    }
}
