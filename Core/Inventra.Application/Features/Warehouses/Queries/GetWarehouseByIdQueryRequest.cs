using Inventra.Application.Common.Results;
using Inventra.Application.Features.Warehouses.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Warehouses.Queries
{
    public class GetWarehouseByIdQueryRequest:IRequest<Result<GetWarehouseByIdQueryResponse>>
    {
        public Guid Id { get; set; }
    }
}
