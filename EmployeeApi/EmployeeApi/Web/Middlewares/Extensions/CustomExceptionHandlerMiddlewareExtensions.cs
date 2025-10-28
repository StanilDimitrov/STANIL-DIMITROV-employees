namespace EmployeeApi.Web.Middlewares.Extensions;

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(
       this IApplicationBuilder builder)
       => builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
}
