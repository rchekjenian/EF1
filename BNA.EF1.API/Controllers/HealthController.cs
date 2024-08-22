using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BNA.EF1.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _service;
        public HealthController(HealthCheckService service)
        {
            _service = service;
        }

        /// <summary>
        /// Método de health de la aplicacion
        /// </summary>
        /// <returns>Estado de los servicios y estado general de la app</returns>
        [HttpGet]
        public async Task<ActionResult> HealthCheck()
        {
            var report = await _service.CheckHealthAsync();

            var exampleHealthCheckValue = report.Entries.FirstOrDefault(x => x.Key == "Example").Value.Status;
            var databaseValue = report.Entries.FirstOrDefault(x => x.Key == "Database").Value.Status;

            return Ok(new
            {
                Services = new
                {
                    ExampleStatus = exampleHealthCheckValue == HealthStatus.Healthy,
                },
                DatabaseStatus = databaseValue == HealthStatus.Healthy,
                GeneralStatus = (exampleHealthCheckValue == HealthStatus.Healthy &&
                                 databaseValue == HealthStatus.Healthy)
            });
        }
    }
}
