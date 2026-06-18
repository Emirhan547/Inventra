using Inventra.WebUI.Common;
using Inventra.WebUI.Dtos.StockMovementDtos;

namespace Inventra.WebUI.Services.StockMovementServices
{

        public interface IStockMovementService
        {
            Task<PagedResponse<ResultStockMovementDto>>GetAllAsync (StockMovementFilterDto filter);
        }
    }

