using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNA.EF1.Infrastructure.Health
{
    public class HealthCheckExampleService : IHealthCheck
    {
        private readonly ILogger<HealthCheckExampleService> _logger;

        public HealthCheckExampleService(ILogger<HealthCheckExampleService> logger)
        {
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Iniciando consulta de salud del servicio Example");

            if (await GenerateRandomValue())
            {
                _logger.LogInformation("Finalizando consulta de salud de servicio Example correctamente");

                return HealthCheckResult.Healthy();
            }

            _logger.LogInformation("Finalizando consulta de salud de servicio Example con errores");

            return HealthCheckResult.Unhealthy();
        }

        private Task<bool> GenerateRandomValue()
        {
            var random = new Random((int)DateTime.Now.Ticks);

            return Task.FromResult(random.Next(1, 6) < 5);
        }
    }
}
