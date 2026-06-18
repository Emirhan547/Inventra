using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Repositories.NotificationRepositories;
using Inventra.Application.Features.Notifications.Queries;
using Inventra.Application.Features.Notifications.Results;
using Inventra.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Notifications.Handlers
{
    public sealed class GetNotificationsQueryHandler: IRequestHandler<GetNotificationsQuery,List<GetNotificationsResponse>>
    {
        private readonly INotificationReadRepository _notificationReadRepository;

        private readonly ICurrentUserService _currentUserService;

        public GetNotificationsQueryHandler( INotificationReadRepository notificationReadRepository,ICurrentUserService currentUserService)
        {
            _notificationReadRepository = notificationReadRepository;
            _currentUserService =currentUserService;
        }

        public async Task<List<GetNotificationsResponse>>Handle(GetNotificationsQuery request,CancellationToken cancellationToken)
        {
            List<Notification> notifications;

            if (_currentUserService.IsAdmin)
            {
                notifications =await _notificationReadRepository.GetAllAsync(false, cancellationToken);
            }
            else
            {
                notifications = await _notificationReadRepository.GetWhereAsync(x =>(x.UserId != null &&x.UserId == _currentUserService.UserId)||(x.RoleName != null && _currentUserService.Roles.Contains(x.RoleName)),false,cancellationToken);
            }

            return notifications.OrderByDescending(x => x.CreatedAt).Take(20).Select(x => new GetNotificationsResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Message = x.Message,
                    Type = x.Type,
                    IsRead = x.IsRead,
                    CreatedAt = x.CreatedAt,
                }).ToList();
        }
    }
   
}