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
    public sealed record GetCuentaQuery(Guid Id) : IRequest<ErrorOr<GetClienteDto>>;


    public sealed class GetCuentaQueryHandler : IRequestHandler<GetCuentaQuery, ErrorOr<GetClienteDto>>
    {
        private readonly ILogger<GetCuentaQueryHandler> _logger;
        private readonly IRepository<Cuenta> _repository;
        private readonly IMapper _mapper;
        public GetCuentaQueryHandler(ILogger<GetCuentaQueryHandler> logger,
            IRepository<Cuenta> repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GetClienteDto>> Handle(GetCuentaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Iniciando consulta de Example por id {Id}", request.Id);

                var cuenta = await _repository.GetAsync(new CuentaByIdSpecification(request.Id), cancellationToken).ConfigureAwait(false);

                if (cuenta == null)
                    return Error.NotFound("GetExampleQuery.ExampleNotFound", $"No example with id {request.Id} was found");

                var getCuentaDto = _mapper.Map<GetClienteDto>(cuenta);

                _logger.LogInformation("Finalizando consulta de Example por id {Id}", request.Id);

                return getCuentaDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public sealed class GetCuentaQueryValidator : AbstractValidator<GetCuentaQuery>
    {
        public GetCuentaQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .Must(BeAValidId)
                .WithMessage("Id must be greater than 0");
        }
         private bool BeAValidId(Guid id)
         {
            return id != default;
         }
    }
}
