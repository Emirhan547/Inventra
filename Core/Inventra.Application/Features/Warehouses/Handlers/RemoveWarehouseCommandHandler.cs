using Inventra.Application.Abstractions.Repositories.WarehouseRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Warehouses.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Warehouses.Handlers
{
    public class RemoveWarehouseCommandHandler(IWarehouseWriteRepository _warehouseWriteRepository,IWarehouseReadRepository _warehouseReadRepository,IUnitOfWork _unitOfWork) : IRequestHandler<RemoveWarehouseCommand, Result>
    {
        public async Task<Result> Handle(RemoveWarehouseCommand request, CancellationToken cancellationToken)
        {
            var wareHouse= await _warehouseReadRepository.GetByIdAsync(request.Id);
            if (wareHouse == null)
            {
                return Result.Failure("Warehouse Not Found");

            }
            _warehouseWriteRepository.Remove(wareHouse);
            await _unitOfWork.SaveChangeAsync();
            return Result.SuccessResult("WareHouse Success Deleted");
        }
    }
}
