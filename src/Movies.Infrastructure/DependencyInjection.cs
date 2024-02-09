using Microsoft.Extensions.DependencyInjection;
using Movies.Domain.Services;
using Movies.Domain.Services.Common;
using Movies.Infrastructure.Services;
using Movies.Infrastructure.Services.Common;

namespace Movies.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddScoped<IHttpClient, CustomHttpClient>();

        services.AddScoped<IMovieService, MovieService>();

        return services;
    }
}
