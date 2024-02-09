using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Features.Movie.Queries.Search;
using System.Reflection;

namespace Movies.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblyContaining<SearchMovieRequest>();
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
