using System.Net;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace dadataStandartizer.ExceptionHandlers;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;
    private readonly IServiceProvider _provider;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger, IServiceProvider provider)
    {
        _next = next;
        _logger = logger;
        _provider = provider;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ExternalException ex)
        {
            await handleException(context, ex, "External error"); // if its external error like 401 from dadata user don't need to know it
        }
        catch (Exception ex)
        {
            await handleException(context, ex);
        }
    }

    private async Task handleException(HttpContext context, Exception ex, string? message = null)
    {
        _logger.LogError(ex, "Intercepted by exception middleware\n");

        var responseObj = _provider.GetService<ErrorResponse>(); // instead of creating object on every request, here we're creating it only for requests with exceptions.

        responseObj.Message = "Something went wrong. Please, notify support with timestamp";
        responseObj.Error = string.IsNullOrEmpty(message) ? ex.Message : message;
        responseObj.Timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
        var response = JsonConvert.SerializeObject(responseObj);
            
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(response);
    }
}

public static class CustomExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CustomExceptionMiddleware>();
    }
}