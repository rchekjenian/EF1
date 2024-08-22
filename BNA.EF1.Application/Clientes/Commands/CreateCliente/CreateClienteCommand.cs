using AutoMapper;
using BNA.EF1.Application.Clientes.Queries.GetCliente;
using BNA.EF1.Application.Clientes.Queries.GetClientesCuentas;
using BNA.EF1.Application.Clientes.Specifications;
using BNA.EF1.Application.Common.Exceptions;
using BNA.EF1.Application.Common.Interfaces;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BNA.EF1.Application.Clientes.Commands.CreateCliente
{
    public sealed record CreateClienteCommand(string Nombre, string Apellido, double cuil) : IRequest<ErrorOr<GetClienteDto>>;

    public sealed class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, ErrorOr<GetClienteDto>>
    {
        private readonly ILogger<CreateClienteCommandHandler> _logger;
        private readonly IRepository<Domain.Clientes.Cliente> _repository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        public CreateClienteCommandHandler(ILogger<CreateClienteCommandHandler> logger,
            IRepository<Domain.Clientes.Cliente> repository, IClienteService clienteService, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _clienteService = clienteService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GetClienteDto>> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Iniciando creacion de Cliente");

                if (!_clienteService.RunCliente())
                    throw new InternalServerErrorException("Cliente Failed");



                var cliente = await _repository.GetAsync(new ClienteByIdSpecification(request.cuil), cancellationToken).ConfigureAwait(false);

                if (cliente == null)
                {
                    var clienteCreationResult = new Domain.Clientes.Cliente(request.Nombre, request.Apellido, request.cuil);

                    //awaut sede ka responsabilidad a otro hilo
                    //cancellationToken es el responsable de facilitar notificaciones de cancelación entre un hilo y otro
                    //ConfigureAwait(false) le informa al nuevo hilo que no espere a que el hilo que lo llamó termine de ejecutar
                    await _repository.AddAsync(clienteCreationResult, cancellationToken).ConfigureAwait(false);

                    _logger.LogInformation("Finalizando creacion del cliente exitosamente");

                    var getClienteDto = _mapper.Map<GetClienteDto>(clienteCreationResult);

                    return getClienteDto;




                }
                else

                {
                    var error = BNA.EF1.Domain.Cliente.Errors.ClienteClassErrors.ClienteExistente(request.cuil);
             
                    return error;
                }

              }
            catch (Exception)
            {
                _logger.LogError("Error al tratar de crear eL cliente");
                throw;
            }
        }
    }

    public sealed class CreateClienteCommandValidator : AbstractValidator<CreateClienteCommand>
    {
        public CreateClienteCommandValidator()
        {
            RuleFor(x => x.Nombre)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nombre null or empty")
                .MaximumLength(50)
                .WithMessage("El nombre debe poseer como maximo 50 caracteres");

            RuleFor(x => x.Apellido)
                .NotNull()
                .NotEmpty()
                .WithMessage("Apellido null or empty")
                .MaximumLength(50)
                .WithMessage("El Apellido debe poseer como maximo 50 caracteres");

            //Agregar validacion para apellido

            RuleFor(x => x.cuil)
                .NotNull()
                .NotEmpty()
                .WithMessage("El cuil no puede ser nulo o vacío")
                .Must(ValidarCuil)
                .WithMessage("El cuil no está bien formado");
        }

        private bool ValidarCuil(double cuil)
        {
            var Cuil = cuil.ToString();
            
            if (Cuil.Length == 11 && (Cuil.StartsWith("20") || Cuil.StartsWith("23") || Cuil.StartsWith("24") || Cuil.StartsWith("27")))
           {
            return true;
            }
            else
            {
            return false;
            }            
        }
    }
}

