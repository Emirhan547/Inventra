using Inventra.Application.Abstractions.Repositories.NotificationRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Notifications.Commands;
using MediatR;

namespace Inventra.Application.Features.Notifications.Handlers
{
    public sealed class MarkNotificationAsReadCommandHandler
        : IRequestHandler<
            MarkNotificationAsReadCommand,
            Result>
    {
        private readonly INotificationReadRepository
            _notificationReadRepository;

        private readonly IUnitOfWork
            _unitOfWork;

        public MarkNotificationAsReadCommandHandler(
            INotificationReadRepository notificationReadRepository,
            IUnitOfWork unitOfWork)
        {
            _notificationReadRepository =
                notificationReadRepository;

            _unitOfWork =
                unitOfWork;
        }

        public async Task<Result> Handle(
            MarkNotificationAsReadCommand request,
            CancellationToken cancellationToken)
        {
            var notification =
                await _notificationReadRepository
                    .GetByIdAsync(
                        request.NotificationId,
                        tracking: true,
                        cancellationToken);

            if (notification is null)
            {
                return Result.Failure(
                    "Bildirim bulunamadı.");
            }

            if (notification.IsRead)
            {
                return Result.SuccessResult();
            }

            notification.IsRead = true;

            await _unitOfWork.SaveChangeAsync();

            return Result.SuccessResult(
                "Bildirim okundu olarak işaretlendi.");
        }
    }
}