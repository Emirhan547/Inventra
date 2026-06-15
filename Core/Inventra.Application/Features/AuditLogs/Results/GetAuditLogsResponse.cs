using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.AuditLogs.Results
{
    public sealed class GetAuditLogsResponse
    {
        public Guid Id { get; set; }

        public string EventName { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public string Description { get; set; } = default!;

        public DateTime OccurredOn { get; set; }
    }
}
