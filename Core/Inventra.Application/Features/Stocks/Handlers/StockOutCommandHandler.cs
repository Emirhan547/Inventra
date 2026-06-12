using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Abstractions.Messaging;
using Inventra.Application.Abstractions.Repositories.StockMovementRepositories;
using Inventra.Application.Abstractions.Repositories.StockRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Contracts.Events;
using Inventra.Application.Features.Stocks.Commands;
using Inventra.Domain.Entities;
using Inventra.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Inventra.Application.Features.Stocks.Handlers
{
    public class StockOutCommandHandler : IRequestHandler<StockOutCommand, Result>
    {
        private readonly IStockReadRepository _stockReadRepository;
        private readonly IStockMovementWriteRepository _stockMovementWriteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<StockOutCommandHandler> _logger;

        public StockOutCommandHandler(
            IStockReadRepository stockReadRepository,
            IStockMovementWriteRepository stockMovementWriteRepository,
            IUnitOfWork unitOfWork,
            INotificationService notificationService,
            IEventBus eventBus,
            ILogger<StockOutCommandHandler> logger)
        {
            _stockReadRepository = stockReadRepository;
            _stockMovementWriteRepository = stockMovementWriteRepository;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task<Result> Handle(
            StockOutCommand request,
            CancellationToken cancellationToken)
        {
            var stock = await _stockReadRepository
                .GetByProductAndWarehouseAsync(
                    request.ProductId,
                    request.WarehouseId,
                    tracking: true,
                    cancellationToken);

            if (stock is null)
            {
                _logger.LogWarning(
                    "Stock not found. ProductId: {ProductId}, WarehouseId: {WarehouseId}",
                    request.ProductId,
                    request.WarehouseId);

                return Result.Failure("Stock not found.");
            }

            if (stock.Quantity < request.Quantity)
            {
                _logger.LogWarning(
                    "Insufficient stock. ProductName: {ProductName}, CurrentQuantity: {CurrentQuantity}, RequestedQuantity: {RequestedQuantity}",
                    stock.Product.Name,
                    stock.Quantity,
                    request.Quantity);

                return Result.Failure("Insufficient stock.");
            }

            stock.Quantity -= request.Quantity;

            await _stockMovementWriteRepository.AddAsync(
                new StockMovement
                {
                    Stock = stock,
                    Quantity = request.Quantity,
                    Type = StockMovementType.StockOut,
                    Description = request.Description
                });

            await _unitOfWork.SaveChangeAsync();

            _logger.LogInformation(
                "Stock out completed. ProductName: {ProductName}, Quantity: {Quantity}, RemainingQuantity: {RemainingQuantity}",
                stock.Product.Name,
                request.Quantity,
                stock.Quantity);

            if (stock.Quantity <= stock.Product.MinimumStockLevel)
            {
                _logger.LogWarning(
                    "Low stock detected. ProductName: {ProductName}, CurrentQuantity: {CurrentQuantity}, MinimumStockLevel: {MinimumStockLevel}",
                    stock.Product.Name,
                    stock.Quantity,
                    stock.Product.MinimumStockLevel);

                await _eventBus.PublishAsync(
                    new LowStockDetectedEvent
                    {
                        ProductId = stock.ProductId,
                        ProductName = stock.Product.Name,
                        CurrentQuantity = stock.Quantity,
                        MinimumStockLevel =
                            stock.Product.MinimumStockLevel
                    });
            }

            return Result.SuccessResult(
                "Stock removed successfully.");
        }
    }
}