using Inventra.Application.Abstractions.Repositories.WarehouseRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Warehouses.Queries;
using Inventra.Application.Features.Warehouses.Results;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Warehouses.Handlers
{
    public class GetWarehouseByIdQueryHandler (IWarehouseReadRepository _warehouseReadRepository): IRequestHandler<GetWarehouseByIdQueryRequest, Result<GetWarehouseByIdQueryResponse>>
    {
        public async Task<Result<GetWarehouseByIdQueryResponse>> Handle(GetWarehouseByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var wareHouse=await _warehouseReadRepository.GetByIdAsync(request.Id);
            if (wareHouse == null)
            {
                return Result<GetWarehouseByIdQueryResponse>.Failure("WareHouse NotFound");
            }
            return Result<GetWarehouseByIdQueryResponse>.SuccessResult(wareHouse.Adapt<GetWarehouseByIdQueryResponse>());
        }
    }
}
