using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MobyLabWebProgramming.Core.Errors;

namespace MobyLabWebProgramming.Infrastructure.Middlewares;

/// <summary>
/// This is the global exception handler/middleware, when a HTTP request arrives it is invoked, if an uncaught exception is caught here it sends a error message back to the client.
/// </summary>
public class GlobalExceptionHandlerMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // Here the next middleware is invoked, the last middleware invoked calls the corresponding controller method for the specified route.
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Caught exception in global exception handler!");

            var response = context.Response;
            response.ContentType = "application/json";

            if (ex is ServerException serverException)
            {
                response.StatusCode = (int)serverException.Status;
                await response.WriteAsync(JsonSerializer.Serialize(ErrorMessage.FromException(serverException)));
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await response.WriteAsync(JsonSerializer.Serialize(new ErrorMessage(HttpStatusCode.InternalServerError, "A unexpected error occurred!")));
            }
        }
    }
}
