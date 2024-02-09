using Movies.Domain.Models;
using Movies.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();

builder.Services.Configure<ImdbCredentials>(
    builder.Configuration.GetSection(ImdbCredentials.CONFIGURATION));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
