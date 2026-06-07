using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.PurchaseOrders.Commands;
using Inventra.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Handlers
{
    public class ApprovePurchaseOrderCommandHandler(
     IPurchaseOrderReadRepository _repository,
     IUnitOfWork _unitOfWork)
     : IRequestHandler<
         ApprovePurchaseOrderCommand,
         Result>
    {
        public async Task<Result> Handle(
            ApprovePurchaseOrderCommand request,
            CancellationToken cancellationToken)
        {
            var purchaseOrder =
                await _repository
                    .GetByIdAsync(
                        request.Id);

            if (purchaseOrder is null)
            {
                return Result.Failure(
                    "Purchase order not found.");
            }

            if (purchaseOrder.Status !=
                PurchaseOrderStatus.Pending)
            {
                return Result.Failure(
                    "Only pending orders can be approved.");
            }

            purchaseOrder.Status =
                PurchaseOrderStatus.Approved;

            await _unitOfWork
                .SaveChangeAsync(
                    );

            return Result.SuccessResult(
                "Purchase order approved.");
        }
    }
}
