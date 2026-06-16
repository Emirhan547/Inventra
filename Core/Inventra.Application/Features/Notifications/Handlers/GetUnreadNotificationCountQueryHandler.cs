using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Repositories.NotificationRepositories;
using Inventra.Application.Features.Notifications.Queries;
using MediatR;

namespace Inventra.Application.Features.Notifications.Handlers
{
    public sealed class
        GetUnreadNotificationCountQueryHandler
        : IRequestHandler<
            GetUnreadNotificationCountQuery,
            int>
    {
        private readonly INotificationReadRepository
            _notificationReadRepository;

        private readonly ICurrentUserService
            _currentUserService;

        public GetUnreadNotificationCountQueryHandler(
            INotificationReadRepository notificationReadRepository,
            ICurrentUserService currentUserService)
        {
            _notificationReadRepository =
                notificationReadRepository;

            _currentUserService =
                currentUserService;
        }

        public async Task<int> Handle(
            GetUnreadNotificationCountQuery request,
            CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAdmin)
            {
                var notifications =
                    await _notificationReadRepository
                        .GetWhereAsync(
                            x => !x.IsRead,
                            false,
                            cancellationToken);

                return notifications.Count;
            }

            var unreadNotifications =
                await _notificationReadRepository
                    .GetWhereAsync(
                        x =>
                            !x.IsRead
                            &&
                            (
                                (x.UserId != null &&
                                 x.UserId ==
                                 _currentUserService.UserId)

                                ||

                                (x.RoleName != null &&
                                 _currentUserService
                                     .Roles
                                     .Contains(
                                         x.RoleName))
                            ),
                        false,
                        cancellationToken);

            return unreadNotifications.Count;
        }
    }
}