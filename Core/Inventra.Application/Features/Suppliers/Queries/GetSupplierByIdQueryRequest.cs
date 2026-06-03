using Inventra.Application.Common.Results;
using Inventra.Application.Features.Suppliers.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Suppliers.Queries
{
    public class GetSupplierByIdQueryRequest:IRequest<Result<GetSupplierByIdQueryResponse>>
    {
        public Guid Id { get; set; }

        public GetSupplierByIdQueryRequest(Guid id)
        {
            Id = id;
        }
    }
}
