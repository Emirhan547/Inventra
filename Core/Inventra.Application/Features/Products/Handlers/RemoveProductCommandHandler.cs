using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Messaging;
using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Contracts.Events;
using Inventra.Application.Features.Products.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Products.Handlers
{
    public class RemoveProductCommandHandler(IProductReadRepository _productRead,IProductWriteRepository _productWrite,IUnitOfWork _unitOfWork,IEventBus _eventBus,
    ICurrentUserService _currentUserService) : IRequestHandler<RemoveProductCommand, Result>
    {
        public async Task<Result> Handle(
     RemoveProductCommand request,
     CancellationToken cancellationToken)
        {
            var product =await _productRead.GetByIdAsync(
                    request.Id);
            if (product == null)
            {
                return Result.Failure("Product Bulunamadı");
            }

            var productName =product.Name;

            _productWrite.Remove(product);

            await _unitOfWork.SaveChangeAsync();

            await _eventBus.PublishAsync(
                new ProductDeletedEvent
                {
                    ProductId = product.Id,
                    ProductName = productName,

                    UserId =
                        _currentUserService.UserId,

                    UserName =
                        _currentUserService.UserName
                });

            return Result.SuccessResult();
        }
    }
}
