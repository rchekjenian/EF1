using BNA.EF1.Application.Common.Exceptions;
using BNA.Logging.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BNA.EF1.API.Filters
{
    public sealed class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;
        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            _logger = logger;

            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(InternalServerErrorException), HandleInternalServerErrorException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            LogResponse(context);

            base.OnException(context);
        }

        private void LogResponse(ExceptionContext context)
        {

            _logger.LogApiRequestResponse(LogLevel.Information,
                "Api Response:" + Environment.NewLine +
                "Status Code: {StatusCode}" + Environment.NewLine +
                "Headers: {@Headers}" + Environment.NewLine +
                "Body: {@Body}",
                context.Result != null ? (context.Result as ObjectResult)!.StatusCode! : context.HttpContext.Response.StatusCode,
                context.HttpContext.Response.Headers.ToDictionary(h => h.Key, h => h.Value),
                context.Result != null ? context.Result : "null"
                );
        }

        private void HandleException(ExceptionContext context)
        {

            Type type = context.Exception.GetType();

            _logger.LogError(context.Exception, "Se detectó una excepción {Name}", type.Name);

            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleInternalServerErrorException(context);
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState);

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleUnauthorizedAccessException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };

            context.ExceptionHandled = true;
        }

        private void HandleInternalServerErrorException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
