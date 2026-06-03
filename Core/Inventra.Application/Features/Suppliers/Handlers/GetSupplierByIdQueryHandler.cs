using Inventra.Application.Abstractions.Repositories.SupplierRepositories;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Suppliers.Queries;
using Inventra.Application.Features.Suppliers.Results;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Suppliers.Handlers
{
    public class GetSupplierByIdQueryHandler(ISupplierReadRepository _supplierReadRepository) : IRequestHandler<GetSupplierByIdQueryRequest, Result<GetSupplierByIdQueryResponse>>
    {
        public async Task<Result<GetSupplierByIdQueryResponse>> Handle(GetSupplierByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var supplier = await _supplierReadRepository.GetSupplierByIdAsync(request.Id);
            if (supplier == null)
            {
                return Result<GetSupplierByIdQueryResponse>.Failure("Supplier Not Found");
            }
            return Result<GetSupplierByIdQueryResponse>.SuccessResult(supplier.Adapt<GetSupplierByIdQueryResponse>());
        }
    }
}
