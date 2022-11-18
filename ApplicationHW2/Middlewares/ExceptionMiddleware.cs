using System.Net;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services;

namespace Middlewares;

internal class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (UserFriendlyException userFriendlyException)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsJsonAsync(userFriendlyException.Message);
        }
        catch(Exception exception)
        {
            _logger.LogError(exception, "Error");
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsJsonAsync("Custom Internal Error");
        }
    }
}
