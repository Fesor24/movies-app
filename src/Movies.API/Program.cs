using Movies.API.Extensions;
using Movies.API.Middleware;
using Movies.Application;
using Movies.Domain.Models;
using Movies.Infrastructure;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Debug()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog();

builder.Services.AddInfrastructureServices()
    .AddApplicationServices();

builder.Services.Configure<ImdbCredentials>(
    builder.Configuration.GetSection(ImdbCredentials.CONFIGURATION));

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSerilogRequestLogging();

app.UseCors("CorsPolicy");

app.RegisterEndpoints();

app.Run();
