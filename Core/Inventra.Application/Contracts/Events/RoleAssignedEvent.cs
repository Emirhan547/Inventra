using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Contracts.Events
{
    public sealed class RoleAssignedEvent
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; } = default!;

        public string RoleName { get; set; } = default!;

        public Guid AssignedByUserId { get; set; }

        public string AssignedByUserName { get; set; } = default!;
    }
}
