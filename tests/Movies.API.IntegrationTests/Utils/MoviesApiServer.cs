using AutoFixture;
using Bogus;
using Movies.Domain.Models;
using Movies.Domain.Primitives;
using System.Text.Json;
using WireMock.Logging;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace Movies.API.IntegrationTests.Utils;
internal class MoviesApiServer : IDisposable
{
    private WireMockServer _server;

    public string Url => _server.Url;

    public void Start() => _server = WireMockServer.Start(new WireMockServerSettings
    {
        Logger = new WireMockConsoleLogger(),
        StartAdminInterface = true,
        Urls = new[] { "http://localhost:59000" },
    });

    public void SearchMovies()
    {
        List<Movie> movies = new Faker<Movie>()
            .RuleFor(x => x.Title, "superman")
            .RuleFor(x => x.ImdbId, faker => Guid.NewGuid().ToString())
            .RuleFor(x => x.Poster, faker => faker.Lorem.Text())
            .RuleFor(x => x.Year, faker => faker.Date.Past(10).Year.ToString())
            .GenerateBetween(3, 7);

        MovieSearchResult searchResult = new()
        {
            Response = "True",
            Search = movies,
            TotalResults = movies.Count.ToString()
        };

        string result = JsonSerializer.Serialize(searchResult);

        _server.Given(Request.Create()
            .WithParam("s", "superman")
            .WithParam("page", "1")
            .UsingGet())
            .RespondWith(Response.Create()
            .WithBody(result)
            .WithHeader("content-type", "application/json")
            .WithStatusCode(System.Net.HttpStatusCode.OK));
    }

    public void GetMovieByImdbId()
    {
        Fixture fixture = new();

        var movie = fixture.Create<MovieItemSearchResult>();

        string result = JsonSerializer.Serialize(new Result<MovieItemSearchResult>(movie));

        _server.Given(Request.Create()
            .WithPath("?apiKey=003939&i=928263637289")
            .UsingGet())
            .RespondWith(Response.Create()
            .WithBody(result)
            .WithHeader("content-type", "application/json")
            .WithStatusCode(System.Net.HttpStatusCode.OK));
    }

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }
}
