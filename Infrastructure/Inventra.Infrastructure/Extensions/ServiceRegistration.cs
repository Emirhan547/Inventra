using Inventra.Application.Abstractions.Infrastructures.IdentityServices;
using Inventra.Application.Abstractions.Infrastructures.JwtServices;
using Inventra.Application.Abstractions.Infrastructures.SignalR;
using Inventra.Application.Abstractions.Messaging;
using Inventra.Infrastructure.Identity;
using Inventra.Infrastructure.Identity.AuthServices;
using Inventra.Infrastructure.JwtServices;
using Inventra.Infrastructure.Messaging;
using Inventra.Infrastructure.SignalR;
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
            services.AddScoped< INotificationService, SignalRNotificationService>();
            services.AddScoped<ICurrentUserService,CurrentUserService>();
            services.AddScoped<IEventBus,MassTransitEventBus>();
            return services;
        }
    }
}
