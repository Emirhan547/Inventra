using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Inventra.Infrastructure.SignalR
{
    [Authorize]
    public sealed class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var roles =
                Context.User?
                    .Claims
                    .Where(x => x.Type == ClaimTypes.Role)
                    .Select(x => x.Value)
                    .ToList();

            if (roles is not null)
            {
                foreach (var role in roles)
                {
                    await Groups.AddToGroupAsync(
                        Context.ConnectionId,
                        role);
                }
            }

            await base.OnConnectedAsync();
        }
    }
}
