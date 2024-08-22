using BNA.EF1.Application.Common.Interfaces;
using BNA.EF1.Infrastructure.Database;
using BNA.EF1.Infrastructure.EventualConsistency;
using BNA.EF1.Infrastructure.ExternalServices;
using BNA.EF1.Infrastructure.Health;
using BNA.EF1.Infrastructure.Repositories;
using BNA.EF1.Infrastructure.Security;
using BNA.Logging.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BNA.EF1.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration, ILoggingBuilder loggingBuilder)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IExampleService), typeof(ExampleService));
            services.AddScoped(typeof(IClienteService), typeof(ClienteService));
            services.AddScoped(typeof(ICuentaService), typeof(CuentaService));

            services.AddScoped(typeof(ISecurityHelper), typeof(SecurityHelper));

            services.AddMemoryCache();

            services.Configure<SecurityOptions>(configuration.GetSection(nameof(SecurityOptions)));

            var securityHelper = services.BuildServiceProvider().GetRequiredService<ISecurityHelper>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ApiDatabase")));

            services.AddHttpContextAccessor();

            services.AddBNALogging(loggingBuilder, settings =>
            {
                settings.AddApiRequestResponseLogging();
                settings.AddExternalLogging();
                settings.AddCorrelationId("__BNA-RequestId");
                settings.AddHttpLoggingBehavior();
            });

         services.AddHealthChecks()
               .AddCheck<HealthCheckExampleService>("Example")

        //       .AddCheck<HealthCheckExampleService>("CLiente")
               .AddDbContextCheck<ApplicationDbContext>("Database");

            return services;
        }

        public static IApplicationBuilder AddMigrations(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

            try
            {
                logger.LogInformation("Iniciando proceso de migración de BD");
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al tratar de generar la migración.");
                throw new Exception("Error al tratar de generar la migración.", ex);
            }

            return app;
        }

        public static IApplicationBuilder AddEventualConsistency(this IApplicationBuilder app)
        {
            app.UseMiddleware<EventualConsistencyMiddleware>();
            return app;
        }
    }
}
