using Inventra.Application.Abstractions.Repositories.CategoryRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Categories.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Categories.Handlers
{
    public class RemoveCategoryCommandHandler (ICategoryReadRepository _categoryReadRepository,ICategoryWriteRepository _categoryWriteRepository,IUnitOfWork _unitOfWork): IRequestHandler<RemoveCategoryCommand, Result>
    {
        public async Task<Result> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryReadRepository.GetByIdAsync(request.Id, tracking: true, cancellationToken);
            if (category == null)
            {
                return Result.Failure("Category Not Found");
            }
            _categoryWriteRepository.Remove(category);
            await _unitOfWork.SaveChangeAsync();
            return Result.SuccessResult();
        }
    }
}
