using BNA.EF1.Domain.Common.Exceptions;
using FluentValidation.Results;

namespace BNA.EF1.Application.Common.Exceptions
{
    public sealed class InternalServerErrorException : CustomException
    {
        public InternalServerErrorException(string message) : base(message) { }

        public InternalServerErrorException(string message, Exception ex) : base(message, ex) { }
    }

    public sealed class ValidationException : CustomException
    {
        public ValidationException() : base("Han ocurrido uno o más errores de validación")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
        public IDictionary<string, string[]> Errors { get; }
    }
}
