using Inventra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        public string EventName { get; set; } = default!;

        public Guid? UserId { get; set; }

        public string UserName { get; set; } = default!;

        public string Description { get; set; } = default!;

        public DateTime OccurredOn { get; set; }
    }
}
