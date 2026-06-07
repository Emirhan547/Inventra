using Inventra.WebUI.Dtos.SupplierDtos;

namespace Inventra.WebUI.Services.SupplierServices
{
    public interface ISupplierService
    {
        Task<List<ResultSupplierDto>> GetAllAsync();

        Task<UpdateSupplierDto?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateSupplierDto model);

        Task UpdateAsync(UpdateSupplierDto model);

        Task DeleteAsync(Guid id);
    }
}
