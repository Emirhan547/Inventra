using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Inventra.Infrastructure.Identity.AuthServices
{
    public class CurrentUserService: ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor =httpContextAccessor;
        }

        public Guid UserId{get
            {
                var value = _httpContextAccessor.HttpContext?.User .FindFirst( ClaimTypes.NameIdentifier)?.Value;
                return Guid.Parse(value!);
            }
        }

        public string UserName => _httpContextAccessor.HttpContext?.User .FindFirst( ClaimTypes.Name)? .Value ?? string.Empty;
    }
}
