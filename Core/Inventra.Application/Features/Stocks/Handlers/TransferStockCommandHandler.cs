using Inventra.Application.Abstractions.Repositories.StockMovementRepositories;
using Inventra.Application.Abstractions.Repositories.StockRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Stocks.Commands;
using Inventra.Domain.Entities;
using Inventra.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Stocks.Handlers
{
    public class TransferStockCommandHandler(IStockReadRepository _stockReadRepository,IUnitOfWork _unitOfWork,IStockMovementWriteRepository _stockMovementWriteRepository,IStockWriteRepository _stockWriteRepository) : IRequestHandler<TransferStockCommand, Result>
    {
        public async Task<Result> Handle(TransferStockCommand request,CancellationToken cancellationToken)
        {
            var sourceStock =await _stockReadRepository.GetByProductAndWarehouseAsync(request.ProductId,request.SourceWarehouseId,tracking: true,cancellationToken);
            if (sourceStock is null)
            {
                return Result.Failure("Source stock not found.");
            }

            if (sourceStock.Quantity < request.Quantity)
            {
                return Result.Failure("Insufficient stock.");
            }
            var destinationStock = await _stockReadRepository.GetByProductAndWarehouseAsync(request.ProductId,request.DestinationWarehouseId,tracking: true,cancellationToken);
            if (destinationStock is null)
            {
                destinationStock = new Stock
                {
                    ProductId = request.ProductId,
                    WarehouseId = request.DestinationWarehouseId,
                    Quantity = 0
                };

                await _stockWriteRepository.AddAsync(destinationStock);
            }

            sourceStock.Quantity -= request.Quantity;

            destinationStock.Quantity += request.Quantity;

            await _stockMovementWriteRepository.AddAsync(new StockMovement
                {
                    Stock = sourceStock,
                    Quantity = request.Quantity,
                    Type = StockMovementType.Transfer,
                    Description =
                        $"Transfer Out - {request.Description}"
                });

            await _stockMovementWriteRepository.AddAsync(new StockMovement
                {
                    Stock = destinationStock,
                    Quantity = request.Quantity,
                    Type = StockMovementType.Transfer,
                    Description =
                        $"Transfer In - {request.Description}"
                });

            await _unitOfWork.SaveChangeAsync();

            return Result.SuccessResult("Stock transferred successfully.");
        }
    }
}
