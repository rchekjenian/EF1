using BNA.EF1.API.Controllers.Base;

using BNA.EF1.Application.Clientes.Queries.GetClientesCuentas;
using BNA.EF1.Application.Clientes.Queries.GetCliente;
using BNA.EF1.Application.Cuentas.Queries.GetCuenta;
using BNA.EF1.Domain.Clientes;
using BNA.EF1.Domain.Cuentas;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BNA.EF1.Application.Clientes.Commands.CreateCliente;
using BNA.EF1.Application.Cuentas.Commands.CreateCuenta;
using ErrorOr;

namespace BNA.EF1.API.Controllers
{
    public class ClienteController : ApiBaseController


    {

        //GET /api/clientes: Obtener la lista de todos los clientes.

        [HttpGet("ListadoCientes")]
        [ProducesResponseType(typeof(List<Application.Clientes.Queries.GetCliente.GetClienteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Application.Clientes.Queries.GetCliente.GetClienteDto>>> GetClientesQuery()
        {
            //var clientes = await Mediator.Send(new GetClientesQuery());
            var clientes = new GetClienteDto(new Guid(), "LoFaro", "Bruno", 20141444443);
            return Ok(clientes);
        }

        //GET /api/clientes/{id}: Obtener detalles de un cliente específico por ID.

        // <param name="id">Cliente id</param>
        // <returns>The Cliente by id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Application.Clientes.Queries.GetCliente.GetClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        private async Task<ActionResult<Application.Clientes.Queries.GetCliente.GetClienteDto>> GetClienteQuery(Guid id)
        {
            var clientes = new GetClienteDto(new Guid(), "LoFaro", "Bruno", 20141444443);
            return Ok(clientes);

        }

        //GET /api/clientes/{cuil}/cuentas: Obtener las cuentas asociadas a un cliente específico.
        [HttpGet("{cuil}/cuentas")]
        [ProducesResponseType(typeof(Application.Clientes.Queries.GetCliente.GetClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Application.Clientes.Queries.GetCliente.GetClienteDto>>> GetClientesCuentasQuery(double cuil)
        {
 //           var result = await Mediator.Send(new GetClientesCuentasQuery(cuil));
            var clientes = new GetClienteDto(new Guid(), "LoFaro", "Bruno", 20141444443);
            return Ok(clientes);


       //     return result.Match(Ok, Problem);
        }
        //POST /api/clientes: Da de alta un cliente.Se debe mandar la información necesaria por body(Nombre, Apellido, CUIL)
        /// <param name="request">ClienteDto</param>
        /// <returns>Cliente</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Application.Clientes.Queries.GetCliente.GetClienteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GetClienteDto>> CreateClienteCommand(CreateClienteCommand  request)
        {
        var result = await Mediator.Send(new CreateClienteCommand( request.Nombre,request.Apellido,request.cuil));

            var clientes = new GetClienteDto(new Guid(), "LoFaro", "Bruno", 20141444443);
            return Ok(clientes);

            
            //    return result.Match(_ => Ok(), Problem);

//            return result.Match(cliente => Ok(cliente), Problem);
            // Si el resultado es exitoso, retornar el DT
            //
            // 
            // Si hay un error, retornar un problema con el código de estado correspondiente
            //-error => Problem(detail: error);

        }



        //POST /api/clientes/{id}/ cuentas: Da de alta una cuenta y la asigna una
        //a un cliente. Se debe la información necesaria por body (Número de cuenta, código de sucursal, saldo y tipo de cuenta)
        /// <param name="request">CuentasDto</param>
        /// <param name="id">ClienteId</param>
        /// <returns>Cuenta</returns>
        [HttpPost("{id}/cuentas")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateCuentaCommandCuenta(Guid id ,[FromBody]CuentaDto request)
        {
            var result = await Mediator.Send(new CreateCuentaCommand(id, request));

            //return result.Match(_ => Ok(), Problem);
            var clientes = new GetClienteDto(new Guid(), "LoFaro", "Bruno", 20141444443);
            return Ok(clientes);
        }


    }
    }







