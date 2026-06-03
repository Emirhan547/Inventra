using Inventra.Application.Abstractions.Repositories.WarehouseRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Warehouses.Commands;
using Inventra.Domain.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Warehouses.Handlers
{
    public class CreateWarehouseCommandHandler (IWarehouseWriteRepository _warehouseWriteRepository,IUnitOfWork _unitOfWork): IRequestHandler<CreateWarehouseCommandRequest, Result<CreateWarehouseCommandResponse>>
    {
        public async Task<Result<CreateWarehouseCommandResponse>> Handle(CreateWarehouseCommandRequest request, CancellationToken cancellationToken)
        {
            var warehouse=request.Adapt<Warehouse>();
            await _warehouseWriteRepository.AddAsync(warehouse);
            await _unitOfWork.SaveChangeAsync();
            return Result<CreateWarehouseCommandResponse>.SuccessResult(
                new CreateWarehouseCommandResponse
                {
                    Id=warehouse.Id
                },
            "Warehouse created successfully");
        }
    }
}
