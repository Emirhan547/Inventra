using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.StockDtos;

namespace Inventra.WebUI.Services.StockServices
{
    public interface IStockService
    {
        Task<PagedResponse<ResultStockDto>>
     GetAllAsync(
         StockFilterDto filter);
        Task StockInAsync(CreateStockInDto model);
        Task StockOutAsync(CreateStockOutDto model);
        Task TransferAsync(CreateTransferStockDto model);
    }
}
