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
    public class StockOutCommandHandler: IRequestHandler<StockOutCommand, Result>
    {
        private readonly IStockReadRepository _stockReadRepository;
        private readonly IStockMovementWriteRepository _stockMovementWriteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StockOutCommandHandler(
            IStockReadRepository stockReadRepository,
            IStockMovementWriteRepository stockMovementWriteRepository,
            IUnitOfWork unitOfWork)
        {
            _stockReadRepository = stockReadRepository;
            _stockMovementWriteRepository = stockMovementWriteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(StockOutCommand request,CancellationToken cancellationToken)
        {
            var stock =await _stockReadRepository.GetByProductAndWarehouseAsync(request.ProductId,request.WarehouseId,tracking: true,cancellationToken);

            if (stock is null)
            {
                return Result.Failure("Stock not found.");
            }

            if (stock.Quantity < request.Quantity)
            {
                return Result.Failure("Insufficient stock.");
            }

            stock.Quantity -= request.Quantity;

            await _stockMovementWriteRepository.AddAsync(new StockMovement
                {
                    Stock = stock,
                    Quantity = request.Quantity,
                    Type = StockMovementType.StockOut,
                    Description = request.Description
                });

            await _unitOfWork.SaveChangeAsync();
            return Result.SuccessResult("Stock removed successfully.");
        }
    }
}
