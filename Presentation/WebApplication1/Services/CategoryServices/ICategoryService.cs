using Inventra.WebUI.Dtos.CategoryDtos;

namespace Inventra.WebUI.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllAsync();

        Task<UpdateCategoryDto?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateCategoryDto model);

        Task UpdateAsync(UpdateCategoryDto model);

        Task DeleteAsync(Guid id);
    }
}
