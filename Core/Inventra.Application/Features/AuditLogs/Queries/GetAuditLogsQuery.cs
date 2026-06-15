using Inventra.Application.Features.AuditLogs.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.AuditLogs.Queries
{
    public sealed class GetAuditLogsQuery
     : IRequest<List<GetAuditLogsResponse>>
    {
    }
}
