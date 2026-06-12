using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Application.Abstractions.Repositories.StockMovementRepositories;
using Inventra.Application.Abstractions.Repositories.StockRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Notifications;
using Inventra.Application.Features.PurchaseOrders.Commands;
using Inventra.Domain.Constants;
using Inventra.Domain.Entities;
using Inventra.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.PurchaseOrders.Handlers
{
    public class CompletePurchaseOrderCommandHandler(IPurchaseOrderReadRepository _repository,IStockReadRepository _stockReadRepository,IStockWriteRepository _stockWriteRepository,IStockMovementWriteRepository _stockMovementWriteRepository,
    IUnitOfWork _unitOfWork, INotificationService _notificationService) : IRequestHandler<CompletePurchaseOrderCommand,Result>
    {
        public async Task<Result> Handle(CompletePurchaseOrderCommand request,CancellationToken cancellationToken)
        {
            var purchaseOrder =await _repository.GetDetailAsync(request.Id,cancellationToken);
            if (purchaseOrder is null)
            {
                return Result.Failure("Purchase order not found.");
            }
           
            foreach (var item in purchaseOrder.Item)
            {
                var stock =await _stockReadRepository.GetByProductAndWarehouseAsync(item.ProductId,request.WarehouseId,true,cancellationToken);

                if (stock is null)
                {
                    stock = new Stock
                    {
                        ProductId = item.ProductId,
                        WarehouseId = request.WarehouseId,
                        Quantity = 0
                    };
               await _stockWriteRepository.AddAsync(stock);
                }
                stock.Quantity += item.Quantity;
                await _stockMovementWriteRepository.AddAsync(new StockMovement
                        {
                            Stock = stock,

                            Quantity = item.Quantity,

                            Type = StockMovementType.StockIn,

                            Description =$"Purchase Order {purchaseOrder.OrderNumber}"
                        });
            }
            if (purchaseOrder is null)
            {
                return Result.Failure("Purchase order not found.");
            }
            if (purchaseOrder.Status !=PurchaseOrderStatus.Approved)
            {
                return Result.Failure("Purchase order must be approved.");
            }
            purchaseOrder.Status =PurchaseOrderStatus.Completed;

            await _unitOfWork.SaveChangeAsync();

            var notification = new NotificationMessage
            {
                Title = "Satın Alma Süreci Tamamlandı",
                Message = $"{purchaseOrder.OrderNumber} numaralı sipariş tamamlandı.",
                Type ="PurchaseOrderCompleted",
                CreatedAt = DateTime.UtcNow
            };

            await _notificationService.SendToRoleAsync(Roles.Employee,notification);

            return Result.SuccessResult( "Purchase order completed.");
        }
    }
}
