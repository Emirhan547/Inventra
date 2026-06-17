using Inventra.Application.Common.Results;
using Inventra.Application.Features.Suppliers.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Suppliers.Queries
{
    public sealed record GetSupplierAiAnalysisQuery(Guid SupplierId)
    : IRequest<Result<GetSupplierAiAnalysisResponse>>;
}
