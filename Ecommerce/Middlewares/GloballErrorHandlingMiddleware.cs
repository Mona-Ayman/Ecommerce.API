using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace Ecommerce.Middlewares
{
    public class GloballErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GloballErrorHandlingMiddleware> _logger;

        public GloballErrorHandlingMiddleware(RequestDelegate next, ILogger<GloballErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                    await HandleNotFoundEndPointAsync(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Something went wrong {exception}");
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private async Task HandleNotFoundEndPointAsync(HttpContext httpContext) // when the user enters not found url (wrong url)
        {
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                statusCode = (int)HttpStatusCode.NotFound,
                errorMessage = $"The end point {httpContext.Request.Path} not found"
            }.ToString();
            await httpContext.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)    // when the user enters not found id 
        {
            //set default status code to 500
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ///set content type => "application/json"
            httpContext.Response.ContentType = "application/json";
            //return standard response
            var response = new ErrorDetails
            {
                errorMessage = exception.Message
            };
            httpContext.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                UnAuthorizedException => (int)HttpStatusCode.Unauthorized,
                ValidationException validationException => HandleValidationException(validationException, response),
                _ => (int)HttpStatusCode.InternalServerError
            }; ;
            response.statusCode = httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsync(response.ToString());
        }

        private int HandleValidationException(ValidationException validationException, ErrorDetails response)
        {
            response.errors = validationException.Errors;
            return (int)HttpStatusCode.BadRequest;
        }
    }
}
