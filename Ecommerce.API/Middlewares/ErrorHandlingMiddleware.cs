using Ecommerce.Entities.Models;
using System.Net;
using System.Text.Json;

namespace Ecommerce.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.ContentType = "application/json";

                HttpStatusCode status;
                string message;

                // select appropriate status code and message based on exception type
                switch (ex)
                {
                    case ArgumentNullException:
                    case ArgumentException:
                        status = HttpStatusCode.BadRequest; // 400
                        message = ex.Message;
                        break;

                    case KeyNotFoundException:
                        status = HttpStatusCode.NotFound; // 404
                        message = ex.Message;
                        break;

                    case UnauthorizedAccessException:
                        status = HttpStatusCode.Unauthorized; // 401
                        message = "Unauthorized access.";
                        break;

                    default:
                        status = HttpStatusCode.InternalServerError; // 500
                        message = "Something went wrong. Please try again later.";
                        break;
                }

                context.Response.StatusCode = (int)status;

                var response = new ApiResponse<string>(message);

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
