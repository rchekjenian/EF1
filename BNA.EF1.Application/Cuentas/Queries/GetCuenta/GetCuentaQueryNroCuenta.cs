using AutoMapper;
using BNA.EF1.Application.Clientes.Queries.GetCliente;
using BNA.EF1.Application.Common.Interfaces;
using BNA.EF1.Application.Cuentas.Specifications;
using BNA.EF1.Domain.Cuentas;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BNA.EF1.Application.Cuentas.Queries.GetCuenta
{
    
    public sealed record GetCuentaQueryNroCuenta(string numeroCuenta) : IRequest<ErrorOr<GetCuentaDto>>;

    public sealed class GetCuentaQueryNroCuentaHandler : IRequestHandler<GetCuentaQueryNroCuenta, ErrorOr<GetCuentaDto>>
    {
        private readonly ILogger<GetCuentaQueryNroCuentaHandler> _logger;
        private readonly IRepository<Cuenta> _repository;
        private readonly IMapper _mapper;
        public GetCuentaQueryNroCuentaHandler(ILogger<GetCuentaQueryNroCuentaHandler> logger,
            IRepository<Cuenta> repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GetCuentaDto>> Handle(GetCuentaQueryNroCuenta request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Iniciando consulta de Example por id {Id}", request.numeroCuenta);

                var cuenta = await _repository.GetAsync(new CuentaByIdSpecification(request.numeroCuenta), cancellationToken).ConfigureAwait(false);

                if (cuenta == null)
                    return Error.NotFound("GetExampleQuery.ExampleNotFound", $"No example with id {request.numeroCuenta} was found");

                var getCuentaDto = _mapper.Map<GetCuentaDto>(cuenta);

                _logger.LogInformation("Finalizando consulta de Example por id {Id}", request.numeroCuenta);

                return getCuentaDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public sealed class GetCuentaQueryNroCuentaValidator : AbstractValidator<GetCuentaQueryNroCuenta>
    {
        public GetCuentaQueryNroCuentaValidator()
        {
            RuleFor(x => x.numeroCuenta);
            //    .NotNull()
            //    .NotEmpty()
            //    .Must(BeAValidId)
            //    .WithMessage("Id must be greater than 0");
        }
        // private bool BeAValidId(Guid id)
        // {
        //    return id != default;
        // }
    }
}
