using AutoFixture;
using Bogus;
using Movies.Domain.Models;
using Movies.Domain.Primitives;
using System.Text.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace Movies.API.IntegrationTests.Utils;
internal class MoviesApiServer : IDisposable
{
    private WireMockServer _server;
    public string Url => _server.Url;

    public void Start() => _server = WireMockServer.Start();

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

        movie.ImdbId = "12345678";

        string result = JsonSerializer.Serialize(movie);

        _server.Given(Request.Create()
            .WithParam("i", movie.ImdbId)
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
