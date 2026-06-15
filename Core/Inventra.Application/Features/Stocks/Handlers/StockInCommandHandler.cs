using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Abstractions.Messaging;
using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Abstractions.Repositories.StockMovementRepositories;
using Inventra.Application.Abstractions.Repositories.StockRepositories;
using Inventra.Application.Abstractions.Repositories.WarehouseRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Contracts.Events;
using Inventra.Application.Features.Notifications;
using Inventra.Application.Features.Stocks.Commands;
using Inventra.Domain.Constants;
using Inventra.Domain.Entities;
using Inventra.Domain.Enums;
using MediatR;

namespace Inventra.Application.Features.Stocks.Handlers;

public class StockInCommandHandler: IRequestHandler<StockInCommand, Result>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IWarehouseReadRepository _warehouseReadRepository;
    private readonly IStockReadRepository _stockReadRepository;
    private readonly IStockWriteRepository _stockWriteRepository;
    private readonly IStockMovementWriteRepository _stockMovementWriteRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notificationService;
    private readonly IEventBus _eventBus;
    public StockInCommandHandler(
        IProductReadRepository productReadRepository,
        IWarehouseReadRepository warehouseReadRepository,
        IStockReadRepository stockReadRepository,
        IStockWriteRepository stockWriteRepository,
        IStockMovementWriteRepository stockMovementWriteRepository,
        IUnitOfWork unitOfWork,
        INotificationService notificationService,
        IEventBus eventBus)
    {
        _productReadRepository = productReadRepository;
        _warehouseReadRepository = warehouseReadRepository;
        _stockReadRepository = stockReadRepository;
        _stockWriteRepository = stockWriteRepository;
        _stockMovementWriteRepository = stockMovementWriteRepository;
        _unitOfWork = unitOfWork;
        _notificationService = notificationService;
        _eventBus = eventBus;
    }

    public async Task<Result> Handle(
     StockInCommand request,
     CancellationToken cancellationToken)
    {
        var product =
            await _productReadRepository.GetByIdAsync(
                request.ProductId
                );

        if (product is null)
        {
            return Result.Failure("Product not found.");
        }

        var warehouseExists =
            await _warehouseReadRepository.AnyAsync(
                x => x.Id == request.WarehouseId,
                cancellationToken);

        if (!warehouseExists)
        {
            return Result.Failure("Warehouse not found.");
        }

        var stock =
            await _stockReadRepository.GetByProductAndWarehouseAsync(
                request.ProductId,
                request.WarehouseId,
                cancellationToken: cancellationToken);

        if (stock is null)
        {
            stock = new Stock
            {
                ProductId = request.ProductId,
                WarehouseId = request.WarehouseId,
                Quantity = 0
            };

            await _stockWriteRepository.AddAsync(stock);
        }

        stock.Quantity += request.Quantity;

        await _stockMovementWriteRepository.AddAsync(
            new StockMovement
            {
                Stock = stock,
                Quantity = request.Quantity,
                Type = StockMovementType.StockIn,
                Description = request.Description
            });

        await _unitOfWork.SaveChangeAsync();

        await _eventBus.PublishAsync(
            new StockInCompletedEvent
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = request.Quantity
            });

        await _eventBus.PublishAsync(
    new DashboardUpdatedEvent());
        return Result.SuccessResult(
            "Stock added successfully.");
    }
}