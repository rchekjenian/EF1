using BNA.EF1.Application.Common.Exceptions;
using BNA.EF1.Application.Common.Interfaces;
using BNA.EF1.Domain.Example;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BNA.EF1.Application.Example.Commands.CreateExample
{
    public sealed record CreateExampleCommand(string ExampleField) : IRequest<ErrorOr<Success>>;

    public sealed class CreateExampleCommandHandler : IRequestHandler<CreateExampleCommand, ErrorOr<Success>>
    {
        private readonly ILogger<CreateExampleCommandHandler> _logger;
        private readonly IRepository<ExampleClass> _repository;
        private readonly IExampleService _exampleService;
        
        public CreateExampleCommandHandler(ILogger<CreateExampleCommandHandler> logger,
            IRepository<ExampleClass> repository, IExampleService exampleService)
        {
            _logger = logger;
            _repository = repository;
            _exampleService = exampleService;
        }

        public async Task<ErrorOr<Success>> Handle(CreateExampleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Iniciando creacion de Example");

                if (!_exampleService.RunExample())
                    throw new InternalServerErrorException("Example Failed");

                var exampleCreationResult = ExampleClass.Create(request.ExampleField, "Internal");

                if (exampleCreationResult.IsError)
                    return exampleCreationResult.FirstError;

                await _repository.AddAsync(exampleCreationResult.Value, cancellationToken).ConfigureAwait(false);

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

    public sealed class CreateExampleCommandValidator : AbstractValidator<CreateExampleCommand>
    {
        public CreateExampleCommandValidator()
        {
            RuleFor(x => x.ExampleField)
                .NotNull()
                .NotEmpty()
                .WithMessage("Example field cannot be null or empty");
                
                    
        }
    }
}

