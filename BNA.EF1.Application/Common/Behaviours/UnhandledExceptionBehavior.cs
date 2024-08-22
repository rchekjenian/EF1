using MediatR;
using Microsoft.Extensions.Logging;

namespace BNA.EF1.Application.Common.Behaviours
{
    public sealed class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogWarning("Excepción no manejada para el Request {Name} {@Request}", requestName, request);

                throw;
            }
        }
    }
}
