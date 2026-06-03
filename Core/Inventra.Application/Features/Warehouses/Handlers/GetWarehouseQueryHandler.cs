using Inventra.Application.Abstractions.Repositories.WarehouseRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Products.Results;
using Inventra.Application.Features.Warehouses.Queries;
using Inventra.Application.Features.Warehouses.Results;
using Inventra.Domain.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Warehouses.Handlers
{
    public class GetWarehouseQueryHandler(IWarehouseReadRepository _warehouseReadRepository) : IRequestHandler <GetWarehouseQueryRequest, Result<List<GetWarehouseQueryResponse>>>
    {
        public async Task<Result<List<GetWarehouseQueryResponse>>> Handle(GetWarehouseQueryRequest request, CancellationToken cancellationToken)
        {
            var wareHouse =await _warehouseReadRepository.GetAllAsync();
            var mapped = wareHouse.Adapt<List<GetWarehouseQueryResponse>>();
            return Result<List<GetWarehouseQueryResponse>>
           .SuccessResult(mapped);
        }
    }
}
