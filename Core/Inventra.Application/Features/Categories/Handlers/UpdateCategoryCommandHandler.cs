using Inventra.Application.Abstractions.Repositories.CategoryRepositories;
using Inventra.Application.Abstractions.Uow;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Categories.Commands;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Categories.Handlers
{
    public class UpdateCategoryCommandHandler (ICategoryReadRepository _categoryReadRepository,ICategoryWriteRepository _categoryWriteRepository,IUnitOfWork _unitOfWork): IRequestHandler<UpdateCategoryCommand, Result>
    {
        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryReadRepository.GetByIdAsync(request.Id, tracking: true);
            if (category == null)
            {
                return Result.Failure("Category Not Found");
            }
            category = request.Adapt(category);
            _categoryWriteRepository.Update(category);
            await _unitOfWork.SaveChangeAsync();
            return Result.SuccessResult("");
        }
    }
}
