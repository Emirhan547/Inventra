using Inventra.WebUI.Dtos.ProfileDtos;

namespace Inventra.WebUI.Services.ProfileServices
{
    public interface IProfileService
    {
        Task<ResultProfileDto> GetAsync();
        Task UpdateAsync(
    UpdateProfileDto model);
        Task ChangePasswordAsync(
    ChangePasswordDto model);


    }
}
