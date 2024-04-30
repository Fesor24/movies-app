using FluentAssertions;
using Movies.API.IntegrationTests.Fixtures;
using Movies.Application.Features.Movie.Queries.GetByImdbId;
using Movies.Application.Features.Movie.Queries.Search;
using Movies.Domain.Primitives;
using System.Net.Http.Json;

namespace Movies.API.IntegrationTests.EndpointTests;

[Collection("Shared")]
public class GetMoviesEndpointTests
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

    [Fact]
    public async Task SearchMovies_WithInvalidSearchTerm_ReturnsBadRequest()
    {
        string url = $"/api/movies/search?searchTerm=&page=1";

        var response = await _httpClient.GetAsync(url);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

        var error = await response.Content.ReadFromJsonAsync<Error>();

        error.Should().NotBeNull();

        error!.Code.Should().Be("MOVIE_SEARCH_TERM");

        error!.Message.Should().Be("Search term can not be null or empty");
    }

    [Fact]
    public async Task Get_GetMovieByImdbId_ReturnMovie()
    {
        string imdbId = "12345678";

        string url = $"/api/movies?imdbId={imdbId}";

        var response = await _httpClient.GetAsync(url);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<GetMovieResponse>();

        result.Should().NotBeNull();

        result!.ImdbId.Should().Be(imdbId);
    }
}
