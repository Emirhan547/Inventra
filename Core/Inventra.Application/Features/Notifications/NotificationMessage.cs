using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Notifications
{
    public sealed class NotificationMessage
    {
        public string Title { get; init; } = default!;

        public string Message { get; init; } = default!;

        public string Type { get; init; } = default!;

        public DateTime CreatedAt { get; init; }
    }
}
