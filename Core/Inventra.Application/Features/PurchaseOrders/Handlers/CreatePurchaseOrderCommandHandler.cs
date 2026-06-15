using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Abstractions.Messaging;
using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Abstractions.Repositories.PurchaseOrderRepositories;
using Inventra.Application.Abstractions.Repositories.SupplierRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Contracts.Events;
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
    public class CreatePurchaseOrderCommandHandler(ISupplierReadRepository _supplierReadRepository, IProductReadRepository _productReadRepository, IPurchaseOrderWriteRepository _purchaseOrderWriteRepository,
    IUnitOfWork _unitOfWork, INotificationService
    _notificationService, IEventBus _eventBus) : IRequestHandler<CreatePurchaseOrderCommandRequest, Result<CreatePurchaseOrderCommandResponse>>
    {
        public async Task<Result<CreatePurchaseOrderCommandResponse>> Handle(CreatePurchaseOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var supplierExists = await _supplierReadRepository.AnyAsync(x => x.Id == request.SupplierId, cancellationToken);
            if (!supplierExists)
            {
                return Result<CreatePurchaseOrderCommandResponse>.Failure("Supplier not found.");
            }
            foreach (var item in request.Items)
            {
                var productExists = await _productReadRepository.AnyAsync(x => x.Id == item.ProductId, cancellationToken);

                if (!productExists)
                {
                    return Result<CreatePurchaseOrderCommandResponse>.Failure($"Product not found : {item.ProductId}");
                }
            }

            var purchaseOrder = new PurchaseOrder
            {
                SupplierId = request.SupplierId,
                OrderNumber = $"PO-{DateTime.UtcNow:yyyy}-{Guid.NewGuid().ToString()[..6].ToUpper()}",
                Status = PurchaseOrderStatus.Pending,
                TotalAmount = request.Items.Sum(
                    x => x.Quantity * x.UnitPrice),
                Item = request.Items
                    .Select(x => new PurchaseOrderItem
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        UnitPrice = x.UnitPrice
                    }).ToList()
            };

            await _purchaseOrderWriteRepository.AddAsync(
    purchaseOrder);

            await _unitOfWork.SaveChangeAsync();

            var notification = new Notification
            {
                Title = "Yeni Satın Alma Talebi",
                Message =$"{purchaseOrder.OrderNumber} numaralı talep oluşturuldu.",
                Type = "PurchaseOrderCreated",
                CreatedAt = DateTime.UtcNow
            };

            await _eventBus.PublishAsync(
    new PurchaseOrderCreatedEvent
    {
        PurchaseOrderId = purchaseOrder.Id,
        OrderNumber = purchaseOrder.OrderNumber
    });
            return Result<CreatePurchaseOrderCommandResponse>.SuccessResult(new CreatePurchaseOrderCommandResponse
                {
                    Id = purchaseOrder.Id
                },
                "Purchase order created successfully.");
        }
    }
}