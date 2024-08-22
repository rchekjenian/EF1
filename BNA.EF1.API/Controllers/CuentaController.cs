using Azure.Core;
using BNA.EF1.API.Controllers.Base;
using BNA.EF1.Application.Clientes.Queries.GetCliente;
using BNA.EF1.Application.Cuentas.Commands.CreateCuenta;
using BNA.EF1.Application.Cuentas.Queries.GetCuenta;
using BNA.EF1.Application.Example.Queries.GetExample;
using BNA.EF1.Domain.Clientes;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BNA.EF1.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CuentaController : ApiBaseController
    {


        /// <param name="numeroCuenta">numeroCuenta</param>
        /// <returns>The Cuenta by numeroCuenta</returns>
        [HttpGet("{numeroCuenta}")]
        [ProducesResponseType(typeof(Application.Clientes.Queries.GetCliente.GetClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Application.Cuentas.Queries.GetCuenta.GetCuentaQueryNroCuenta>> GetCuentaQueryNroCuenta(string numeroCuenta)
        {
            var results = await Mediator.Send(new GetCuentaQueryNroCuenta(numeroCuenta));

            return results.Match(
                cuentas => Ok(cuentas), // Devuelve un Ok con la lista de cuentas
                problem => Problem(problem)      // Devuelve un problema si hay algún error
            );
        }








    }
}