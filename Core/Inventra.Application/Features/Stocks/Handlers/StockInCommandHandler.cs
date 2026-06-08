using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Abstractions.Repositories.StockMovementRepositories;
using Inventra.Application.Abstractions.Repositories.StockRepositories;
using Inventra.Application.Abstractions.Repositories.WarehouseRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Stocks.Commands;
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

    public StockInCommandHandler(
        IProductReadRepository productReadRepository,
        IWarehouseReadRepository warehouseReadRepository,
        IStockReadRepository stockReadRepository,
        IStockWriteRepository stockWriteRepository,
        IStockMovementWriteRepository stockMovementWriteRepository,
        IUnitOfWork unitOfWork)
    {
        _productReadRepository = productReadRepository;
        _warehouseReadRepository = warehouseReadRepository;
        _stockReadRepository = stockReadRepository;
        _stockWriteRepository = stockWriteRepository;
        _stockMovementWriteRepository = stockMovementWriteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(StockInCommand request,CancellationToken cancellationToken)
    {
        var productExists =await _productReadRepository.AnyAsync(x => x.Id == request.ProductId,cancellationToken);
        if (!productExists)
        {
            return Result.Failure("Product not found.");
        }
        var warehouseExists =await _warehouseReadRepository.AnyAsync( x => x.Id == request.WarehouseId,cancellationToken);

        if (!warehouseExists)
        {
            return Result.Failure("Warehouse not found.");
        }
        var stock =await _stockReadRepository.GetByProductAndWarehouseAsync( request.ProductId, request.WarehouseId,cancellationToken: cancellationToken);

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

        await _stockMovementWriteRepository.AddAsync(new StockMovement
            {
                Stock = stock,
                Quantity = request.Quantity,
                Type = StockMovementType.StockIn,
                Description = request.Description
            });
        await _unitOfWork.SaveChangeAsync();
        return Result.SuccessResult("Stock added successfully.");
    }
}