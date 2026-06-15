using Inventra.Application.Features.Notifications.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Notifications.Queries
{
    public sealed record GetNotificationsQuery
     : IRequest<List<GetNotificationsResponse>>;
    
    
}
