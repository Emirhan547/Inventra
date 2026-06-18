using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Profiles.Queries;
using Inventra.Application.Features.Profiles.Results;
using MediatR;

namespace Inventra.Application.Features.Profile.Handlers;

public class GetProfileQueryHandler: IRequestHandler<GetProfileQuery, Result<GetProfileQueryResponse>>
{
    private readonly IProfileService _profileService;

    public GetProfileQueryHandler(IProfileService profileService)
    {
        _profileService = profileService;
    }

    public async Task<Result<GetProfileQueryResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        return await _profileService.GetProfileAsync();
    }
}
    
