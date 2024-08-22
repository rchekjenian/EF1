using BNA.EF1.Application.Common.Exceptions;
using BNA.EF1.Application.Common.Interfaces;
using BNA.EF1.Domain.Clientes;
using BNA.EF1.Domain.Cuentas;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BNA.EF1.Application.Cuentas.Commands.CreateCuenta
{       
    
    public sealed record CuentaDto(string NumeroCuenta, string CodigoSucursal, int TipoCuenta, double Saldo);

    //, Guid ClienteId
    public sealed record CreateCuentaCommand(Guid Id,   CuentaDto Cuenta ) : IRequest<ErrorOr<Success>>;

    public sealed class CreateCuentaCommandHandler : IRequestHandler<CreateCuentaCommand, ErrorOr<Success>>
    
    {
     
        private readonly ILogger<CreateCuentaCommandHandler> _logger;
        private readonly IRepository<Domain.Cuentas.Cuenta> _repository;
        private readonly ICuentaService _cuentaService;

        public CreateCuentaCommandHandler(ILogger<CreateCuentaCommandHandler> logger,
            IRepository<Domain.Cuentas.Cuenta> repository,
            ICuentaService cuentaService)
        {
            _logger = logger;
            _repository = repository;
            _cuentaService = cuentaService;
        }

        public async Task<ErrorOr<Success>> Handle(CreateCuentaCommand  request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Iniciando creacion de Cuenta");

                if (!_cuentaService.RunCuenta())
                    throw new InternalServerErrorException("Cuenta Failed");

                //revisar si el cliente ya existe

                //si existe devolver un error de dominio

                //if (clienteValidationResult.IsError)
                //    return clienteCreationResult.FirstError;


                //si no existe request.ClienteId
                var cuentaCreationResult = new Domain.Cuentas.Cuenta(request.Cuenta.NumeroCuenta, request.Cuenta.CodigoSucursal, request.Cuenta.TipoCuenta, request.Cuenta.Saldo, request.Id);
                await _repository.AddAsync(cuentaCreationResult, cancellationToken).ConfigureAwait(false);


                //cuentaCreationResult.ClienteId = request.clienteId;
                //awaut sede ka responsabilidad a otro hilo
                //cancellationToken es el responsable de facilitar notificaciones de cancelación entre un hilo y otro
                //ConfigureAwait(false) le informa al nuevo hilo que no espere a que el hilo que lo llamó termine de ejecutar
                //     await _repository.AddAsync(cuentaCreationResult, cancellationToken).ConfigureAwait(false);

                _logger.LogInformation("Finalizando creacion de Example exitosamente");

                return Result.Success;

            }
            catch (Exception)
            {
                _logger.LogError("Error al tratar de crear el example");
                throw;
            }
        }
    }

    public sealed class CreateCuentaCommandValidator : AbstractValidator<CreateCuentaCommand>
    {
        public CreateCuentaCommandValidator()
        {
            RuleFor(x => x.Cuenta.NumeroCuenta)
               .NotNull()
               .NotEmpty()
               .WithMessage("Cuenta null or empty")
               .MaximumLength(10)
               .MinimumLength(10)
               .WithMessage("el numero de cuenta debe poseer 10 caracteres");
            RuleFor(x => x.Cuenta.CodigoSucursal)
              .NotNull()
              .NotEmpty()
              .WithMessage("Cuenta field cannot be null or empty")
              .MaximumLength(4)
              .MinimumLength(4)
              .WithMessage("el codigo de sucursal debe contener 4 caracteres");
               RuleFor(x => x.Cuenta.TipoCuenta)
               .Must(BeValidTipoCuenta)
               .WithMessage("TipoCuenta debe ser '20' o '21'.");
            

            
        }

    private bool BeValidTipoCuenta(int tipoCuenta)
        {
            return tipoCuenta == 20 || tipoCuenta == 21;
        }

    //Agregar validacion para apellido

    //            RuleFor(x => x.cuil)
    //              .NotNull()
    //            .NotEmpty()
    //          .WithMessage("El cuil no puede ser nulo o vacío")
    //        .Must(ValidarCuil)
    //      .WithMessage("El cuil no está bien formado");


        // private bool ValidarCuil(double cuil)
        //{
        //var Cuil = cuil.ToString();
        //   if (Cuil.Length == 11 && (Cuil.StartsWith("20") || Cuil.StartsWith("23") || Cuil.StartsWith("24") || Cuil.StartsWith("27")))
        //  {
        //     return true;
        //}
        //       else
        //   {
        //         return false;
        //}



        //Validar que el cuil tenga el largo correcto
        //Validar con que empieza el cuil (20, 23, 24, 27)

        //}
    }
}

