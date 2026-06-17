using Inventra.Application.Common.Results;
using Inventra.Application.Features.Profiles.Commands;
using Inventra.Application.Features.Profiles.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Infrastructures.IdentityServices
{
    public interface IProfileService
    {
        Task<Result<GetProfileQueryResponse>> GetProfileAsync();
        Task<Result> UpdateProfileAsync(
    UpdateProfileCommandRequest request);
        Task<Result> ChangePasswordAsync(
    ChangePasswordCommandRequest request);


    }
}
