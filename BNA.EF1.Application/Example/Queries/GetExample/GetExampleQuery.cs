using AutoMapper;
using BNA.EF1.Application.Common.Interfaces;
using BNA.EF1.Application.Example.Specifications;
using BNA.EF1.Domain.Example;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BNA.EF1.Application.Example.Queries.GetExample
{
    public sealed record GetExampleQuery(Guid Id) : IRequest<ErrorOr<GetExampleDto>>;

    public sealed class GetExampleQueryHandler : IRequestHandler<GetExampleQuery, ErrorOr<GetExampleDto>>
    {
        private readonly ILogger<GetExampleQueryHandler> _logger;
        private readonly IRepository<ExampleClass> _repository;
        private readonly IMapper _mapper;
        public GetExampleQueryHandler(ILogger<GetExampleQueryHandler> logger,
            IRepository<ExampleClass> repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GetExampleDto>> Handle(GetExampleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Iniciando consulta de Example por id {Id}", request.Id);

                var example = await _repository.GetAsync(new ExampleByIdSpecification(request.Id), cancellationToken).ConfigureAwait(false);

                if (example == null)
                    return Error.NotFound("GetExampleQuery.ExampleNotFound", $"No example with id {request.Id} was found");

                var getExampleDto = _mapper.Map<GetExampleDto>(example);

                _logger.LogInformation("Finalizando consulta de Example por id {Id}", request.Id);

                return getExampleDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public sealed class GetExampleQueryValidator : AbstractValidator<GetExampleQuery>
    {
        public GetExampleQueryValidator()
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
