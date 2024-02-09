using Movies.API.Extensions;
using Movies.Application;
using Movies.Domain.Models;
using Movies.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices()
    .AddApplicationServices();

builder.Services.Configure<ImdbCredentials>(
    builder.Configuration.GetSection(ImdbCredentials.CONFIGURATION));

var app = builder.Build();

app.RegisterEndpoints();

app.Run();
