using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Notifications.Results
{
    public sealed class GetNotificationsResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = default!;

        public string Message { get; set; } = default!;

        public string Type { get; set; } = default!;

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
