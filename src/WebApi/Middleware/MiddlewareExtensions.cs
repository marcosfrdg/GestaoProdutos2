using Microsoft.AspNetCore.Builder;

namespace Api.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
                    => app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}