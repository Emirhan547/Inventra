using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Abstractions.Messaging;
using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Contracts.Events;
using Inventra.Application.Features.Notifications;
using Inventra.Application.Features.PurchaseOrders.Commands;
using Inventra.Domain.Constants;
using Inventra.Domain.Enums;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Handlers
{
    public class ApprovePurchaseOrderCommandHandler(IPurchaseOrderReadRepository _repository,IUnitOfWork _unitOfWork
    , IEventBus _eventBus, ICurrentUserService _currentUserService, ILogger<ApprovePurchaseOrderCommandHandler> _logger) : IRequestHandler<ApprovePurchaseOrderCommand,Result>
    {

        public async Task<Result> Handle(ApprovePurchaseOrderCommand request,CancellationToken cancellationToken)
        {
            var purchaseOrder =await _repository.GetByIdAsync(request.Id);

            if (purchaseOrder is null)
            {
                return Result.Failure("Purchase order not found.");
            }
            if (purchaseOrder.Status !=PurchaseOrderStatus.Pending)
            {
                return Result.Failure("Only pending orders can be approved.");
            }
            purchaseOrder.Status =PurchaseOrderStatus.Approved;
            await _unitOfWork.SaveChangeAsync();
            _logger.LogInformation(
    "Purchase order approved. OrderNumber: {OrderNumber}, ApprovedBy: {UserName}",
    purchaseOrder.OrderNumber,
    _currentUserService.UserName);

            await _eventBus.PublishAsync(
    new PurchaseOrderApprovedEvent
    {
        PurchaseOrderId = purchaseOrder.Id,
        OrderNumber = purchaseOrder.OrderNumber,

        UserId = _currentUserService.UserId,
        UserName = _currentUserService.UserName
    });
            return Result.SuccessResult("Purchase order approved.");
        }
    }
}
