using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.ComponentModel.DataAnnotations;

namespace Engie.Application
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
#pragma warning disable CS8604 // Possible null reference argument.
                config = config.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(AssemblyPointer) ?? throw new Exception("Assembly not found")));

            });
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<AssemblyPointer>(includeInternalTypes: true);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
#pragma warning restore CS8604 // Possible null reference argument.
        }
    }
}
