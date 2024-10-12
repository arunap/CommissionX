using System.Net;
using System.Text.Json;
using CommissionX.Api.Models;

namespace CommissionX.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Proceed to the next middleware in the pipeline
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var response = new ErrorResponseDto
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = "Unauthorized Access Error Occured.",
                    Detailed = ex.Message
                };

                _logger.LogError(ex, "Unauthorized Access Error Occured.");

                // Serialize and return the response as JSON
                var responseJson = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(responseJson);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError($"Something went wrong: {ex.Message}");

                // Handle the exception
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Set the response status code
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // Create a response object with error details
            var response = new ErrorResponseDto
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the middleware.",
                Detailed = exception.Message
            };

            // Serialize and return the response as JSON
            var responseJson = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(responseJson);
        }
    }
}