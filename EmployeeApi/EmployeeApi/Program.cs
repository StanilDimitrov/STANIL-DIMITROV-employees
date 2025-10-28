using CsvHelper.Configuration;
using EmployeeApi.Application.Csv;
using EmployeeApi.Application.Csv.Options;
using EmployeeApi.Application.Services;
using EmployeeApi.Web.Middlewares.Extensions;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IEmployeeProjectCsvDeserializer, EmployeeProjectCsvDeserializer>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.Configure<CsvConfig>(builder.Configuration.GetSection(nameof(CsvConfig)));

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .WithExposedHeaders("Content-Disposition");
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCustomExceptionHandler();

        app.UseRouting();

        app.UseCors("AllowAngularApp");

        app.MapControllers();

        await app.RunAsync();
    }
}
