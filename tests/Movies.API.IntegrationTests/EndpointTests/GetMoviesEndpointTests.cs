using FluentAssertions;
using Movies.API.IntegrationTests.Fixtures;
using Movies.Application.Features.Movie.Queries.Search;
using System.Net.Http.Json;

namespace Movies.API.IntegrationTests.EndpointTests;
public class GetMoviesEndpointTests : IClassFixture<MovieApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public GetMoviesEndpointTests(MovieApplicationFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task SearchMovies_WithValidSearchTerm_ReturnsListOfMovies()
    {
        string url = $"/api/movies/search?searchTerm=superman&page=1";

        var response = await _httpClient.GetAsync(url);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<SearchMovieResponse>();

        result.Should().NotBeNull();

        result!.TotalRecords.Should().BeGreaterThan(1);

        result!.Movies.Count.Should().Be(result.TotalRecords);

        result!.Movies.ForEach(movie => movie.Title.Contains("superman"));
    }
}
