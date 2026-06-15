using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Messaging;
using Inventra.Application.Abstractions.Repositories.ProductRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Contracts.Events;
using Inventra.Application.Features.Products.Commands;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Products.Handlers
{
    public class UpdateProductCommandHandler (IProductReadRepository _readRepository,
    IProductWriteRepository _writeRepository,
    IUnitOfWork _unitOfWork,
    IEventBus _eventBus,
    ICurrentUserService _currentUserService) : IRequestHandler<UpdateProductCommand, Result>
    {
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _readRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return Result.Failure("Product Bulunamadı");

            }
            product = request.Adapt(product);
            _writeRepository.Update(product);
             await _unitOfWork.SaveChangeAsync();
            await _eventBus.PublishAsync(
    new ProductUpdatedEvent
    {
        ProductId = product.Id,
        ProductName = product.Name,

        UserId = _currentUserService.UserId,
        UserName = _currentUserService.UserName
    });
            return Result.SuccessResult();
        }
    }
}
