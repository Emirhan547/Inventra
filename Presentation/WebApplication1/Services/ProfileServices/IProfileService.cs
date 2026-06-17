using Inventra.WebUI.Dtos.ProfileDtos;

namespace Inventra.WebUI.Services.ProfileServices
{
    public interface IProfileService
    {
        Task<ResultProfileDto> GetAsync();
        Task<string> UpdateAsync(
    UpdateProfileDto model);
        Task<string> ChangePasswordAsync(
    ChangePasswordDto model);


    }
}
