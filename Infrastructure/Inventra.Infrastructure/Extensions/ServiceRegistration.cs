using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Infrastructures.JwtServices;
using Inventra.Infrastructure.Identity;
using Inventra.Infrastructure.Identity.AuthServices;
using Inventra.Infrastructure.JwtServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService,CurrentUserService>();
            return services;
        }
    }
}
