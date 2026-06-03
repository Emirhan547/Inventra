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
    public class GetSuppliersQueryHandler (ISupplierReadRepository _supplierReadRepository): IRequestHandler<GetSuppliersQueryRequest, Result<List<GetSuppliersQueryResponse>>>
    {
        public async Task<Result<List<GetSuppliersQueryResponse>>> Handle(GetSuppliersQueryRequest request, CancellationToken cancellationToken)
        {
            var supliers = await _supplierReadRepository.GetSuppliersAsync(cancellationToken);
            if (supliers == null)
            {
                return Result<List<GetSuppliersQueryResponse>>.Failure("Supplier Not Found");
            }
            return Result<List<GetSuppliersQueryResponse>>.SuccessResult(supliers.Adapt<List<GetSuppliersQueryResponse>>());
        }
    }
}
