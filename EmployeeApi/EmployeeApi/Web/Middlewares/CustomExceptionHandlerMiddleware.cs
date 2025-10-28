using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace EmployeeApi.Web.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public CustomExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "{Message}", exception.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Generic exception",
                Detail = "Unknown error occurred.",
                Instance = context.Request.Path
            };
            string responseAsString = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(responseAsString);
        }
    }
}
