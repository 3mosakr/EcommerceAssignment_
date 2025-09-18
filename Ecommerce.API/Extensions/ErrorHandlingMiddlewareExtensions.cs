using Ecommerce.API.Middlewares;

namespace Ecommerce.API.Extensions
{
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
