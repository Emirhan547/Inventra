using FluentValidation;
using Inventra.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inventra.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddApplicationExt(
            this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(
                    Assembly.GetExecutingAssembly());
            });

            services.AddValidatorsFromAssembly(
                Assembly.GetExecutingAssembly());

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>));

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(PerformanceBehavior<,>));
        }
    }
}