using Inventra.WebUI.Dtos.WarehouseDtos;

namespace Inventra.WebUI.Services.WarehouseServices
{
    public interface IWarehouseService
    {
        Task<List<ResultWarehouseDto>> GetAllAsync();

        Task<UpdateWarehouseDto?> GetByIdAsync(Guid id);

        Task CreateAsync(CreateWarehouseDto model);

        Task UpdateAsync(UpdateWarehouseDto model);

        Task DeleteAsync(Guid id);
    }
}
