using Inventra.Application.Abstractions.Repositories.NotificationRepositories;
using Inventra.Application.Features.Notifications.Queries;
using Inventra.Application.Features.Notifications.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Notifications.Handlers
{
    public sealed class GetNotificationsQueryHandler
    : IRequestHandler<
        GetNotificationsQuery,
        List<GetNotificationsResponse>>
    {
        private readonly INotificationReadRepository
            _notificationReadRepository;

        public GetNotificationsQueryHandler(
            INotificationReadRepository notificationReadRepository)
        {
            _notificationReadRepository =
                notificationReadRepository;
        }

        public async Task<List<GetNotificationsResponse>>
    Handle(
        GetNotificationsQuery request,
        CancellationToken cancellationToken)
        {
            var notifications =
                await _notificationReadRepository
                    .GetAllAsync(false);

            return notifications
                .OrderByDescending(x => x.CreatedAt)
                .Take(20)
                .Select(x =>
                    new GetNotificationsResponse
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Message = x.Message,
                        Type = x.Type,
                        IsRead = x.IsRead,
                        CreatedAt = x.CreatedAt
                    })
                .ToList();
        }
    }
}