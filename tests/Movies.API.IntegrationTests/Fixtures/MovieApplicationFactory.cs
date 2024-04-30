using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Movies.API.IntegrationTests.Utils;
using Movies.Application.Services;
using Movies.Infrastructure.Services;

namespace Movies.API.IntegrationTests.Fixtures;
public class MovieApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly MoviesApiServer _apiServer = new();

    public Task InitializeAsync()
    {
        _apiServer.Start();

        _apiServer.SearchMovies();

        //_apiServer.GetMovieByImdbId();

        return Task.CompletedTask;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddHttpClient<IMovieService, MovieService>(client =>
            {
                client.BaseAddress = new Uri(_apiServer.Url);
            });
        });
    }

    public new Task DisposeAsync()
    {
        _apiServer.Dispose();

        return Task.CompletedTask;
    }
}
