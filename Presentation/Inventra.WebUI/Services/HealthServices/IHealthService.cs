using Inventra.WebUI.Dtos.HealthDtos;

namespace Inventra.WebUI.Services.HealthServices
{
    public interface IHealthService
    {
        Task<HealthResponseDto>
            GetStatusAsync();
    }
}
