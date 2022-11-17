using Microsoft.AspNetCore.Builder;

namespace Middlewares;

public static class ExceptionMiddlewareExtensions
{
    public static void UseExceptionHandling(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<ExceptionMiddleware>();
    }
}