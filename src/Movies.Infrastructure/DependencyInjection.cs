using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Services;
using Movies.Infrastructure.Services;

namespace Movies.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddHttpClient<IMovieService, MovieService>(client =>
        {
            client.BaseAddress = new Uri("http://www.omdbapi.com");
        });

        return services;
    }
}
