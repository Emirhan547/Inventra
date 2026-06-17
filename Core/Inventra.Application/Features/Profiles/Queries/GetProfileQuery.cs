using Inventra.Application.Common.Results;
using Inventra.Application.Features.Profiles.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Profiles.Queries
{
    public sealed record GetProfileQuery
     : IRequest<Result<GetProfileQueryResponse>>;
}