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
    public sealed record GetCuentas() : IRequest<ErrorOr<GetClienteDto>>;


    public sealed class GetCuentasHandler : IRequestHandler<GetCuentas, ErrorOr<GetClienteDto>>
    {
        private readonly ILogger<GetCuentasHandler> _logger;
        private readonly IRepository<Cuenta> _repository;
        private readonly IMapper _mapper;
        public GetCuentasHandler(ILogger<GetCuentasHandler> logger,
            IRepository<Cuenta> repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GetClienteDto>> Handle(GetCuentas request, CancellationToken cancellationToken)
        {
            try
            {
                
                var cuenta = await _repository.GetAsync(new CuentaByIdSpecification(), cancellationToken).ConfigureAwait(false);
                var getCuentaDto = _mapper.Map<GetClienteDto>(cuenta);
                return getCuentaDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public sealed class GetCuentasValidator : AbstractValidator<GetCuentas>
    {
        public GetCuentasValidator()
        {   //    .NotNull()
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
