using Inventra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Domain.Entities
{
    public sealed class Notification : BaseEntity
    {
        public Guid UserId { get; set; }

        public string Title { get; set; } = default!;

        public string Message { get; set; } = default!;

        public string Type { get; set; } = default!;

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
