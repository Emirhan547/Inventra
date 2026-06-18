using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Repositories.NotificationRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Notifications.Commands;
using Inventra.Domain.Entities;
using MediatR;

namespace Inventra.Application.Features.Notifications.Handlers
{
    public sealed class MarkAllNotificationsAsReadCommandHandler: IRequestHandler<MarkAllNotificationsAsReadCommand, Result>
    {
        private readonly INotificationReadRepository _notificationReadRepository;

        private readonly ICurrentUserService _currentUserService;

        private readonly IUnitOfWork _unitOfWork;

        public MarkAllNotificationsAsReadCommandHandler(INotificationReadRepository notificationReadRepository,ICurrentUserService currentUserService,IUnitOfWork unitOfWork)
        {
            _notificationReadRepository =notificationReadRepository;
            _currentUserService =currentUserService;
            _unitOfWork =unitOfWork;
        }

        public async Task<Result> Handle(MarkAllNotificationsAsReadCommand request,CancellationToken cancellationToken)
        {
            List<Notification> notifications;

            if (_currentUserService.IsAdmin)
            {
                notifications =await _notificationReadRepository.GetAllAsync(true,cancellationToken);
            }
            else
            {
                notifications =await _notificationReadRepository.GetWhereAsync(x =>
                                (x.UserId != null && x.UserId == _currentUserService.UserId)
                                ||
                                (x.RoleName != null && _currentUserService.Roles.Contains(x.RoleName)),true,cancellationToken);
            }
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }
            await _unitOfWork.SaveChangeAsync();
            return Result.SuccessResult("Tüm bildirimler okundu olarak işaretlendi.");
        }
    }
}