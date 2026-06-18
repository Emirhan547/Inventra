using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Common.Results;
using Inventra.Application.Features.Profiles.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Profiles.Handlers
{
    public sealed class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommandRequest, Result>
    {
        private readonly IProfileService _profileService;

        public UpdateProfileCommandHandler( IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<Result> Handle(UpdateProfileCommandRequest request,CancellationToken cancellationToken)
        {
            return await _profileService.UpdateProfileAsync(request);
        }

    }
}