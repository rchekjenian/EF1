using BNA.EF1.Application.Common.Behaviours;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BNA.EF1.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                options.AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>));
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));

            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //services.Configure<SearchAccountOptions>(configuration.GetSection(nameof(SearchAccountOptions))); --> para configurar opciones
            
            return services;
        }
    }
}
