using AutoMapper;
using BNA.EF1.Application.Clientes.Specifications;
using BNA.EF1.Application.Common.Interfaces;
using BNA.EF1.Domain.Clientes;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BNA.EF1.Application.Clientes.Queries.GetCliente
{
    public sealed record GetClienteCuilQuery(double cuil) : IRequest<ErrorOr<GetClienteDto>>;

    public sealed class GetClienteCuilQueryHandler : IRequestHandler<GetClienteCuilQuery, ErrorOr<GetClienteDto>>
    {
        private readonly ILogger<GetClienteCuilQueryHandler> _logger;
        private readonly IRepository<Cliente> _repository;
        private readonly IMapper _mapper;
        public GetClienteCuilQueryHandler(ILogger<GetClienteCuilQueryHandler> logger,
            IRepository<Cliente> repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<GetClienteDto>> Handle(GetClienteCuilQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Iniciando consulta de Example por id {Id}", request.cuil);

                var cliente = await _repository.GetAsync(new ClienteByIdSpecification(request.cuil), cancellationToken).ConfigureAwait(false);

                if (cliente == null)
                    return Error.NotFound("GetExampleQuery.ExampleNotFound", $"No example with id {request.cuil} was found");
                var getClienteDto = _mapper.Map<GetClienteDto>(cliente);
                _logger.LogInformation("Finalizando consulta de Example por id {Id}", request.cuil);
                return getClienteDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public sealed class GetClienteCuilQueryValidator : AbstractValidator<GetClienteCuilQuery>
    {
        public GetClienteCuilQueryValidator()
        {
            RuleFor(x => x.cuil)
                .NotNull()
                .NotEmpty()
                .Must(BeAValidId)
                .WithMessage("Id must be greater than 0");
        }
        private bool BeAValidId(double cuil)
        {
            return cuil != default;
        }
    }
}
