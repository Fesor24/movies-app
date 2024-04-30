using Microsoft.Extensions.Options;
using Movies.Application.Services;
using Movies.Domain.Models;
using Movies.Domain.Primitives;
using System.Net.Http.Json;

namespace Movies.Infrastructure.Services;
public class MovieService : IMovieService
{
    private readonly HttpClient _httpClient;
    private readonly string _url;

    public MovieService(HttpClient httpClient, IOptions<ImdbCredentials> credentials)
    {
        _httpClient = httpClient;
        _url = httpClient.BaseAddress + $"?apiKey={credentials.Value.ApiKey}";
    }

    public async Task<Result<MovieItemSearchResult>> GetMovieByImdbId(string imdbId)
    {
        string url = _url + $"&i={imdbId}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = response.ReasonPhrase;

            var errorDetails = await response.Content.ReadAsStringAsync();

            return new Error("GET_MOVIE_BY_ID", $"{errorMessage} \n {errorDetails}");
        }

        var movie = await response.Content.ReadFromJsonAsync<MovieItemSearchResult>();

        return movie;
    }

    public async Task<Result<MovieSearchResult>> Search(string searchTerm, int page)
    {
        string url = _url + $"&s={searchTerm}&page={page}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = response.ReasonPhrase;

            var errorDetails = await response.Content.ReadAsStringAsync();

            return new Error("SEARCH_MOVIES", $"{errorMessage} \n {errorDetails}");
        }

        var movies = await response.Content.ReadFromJsonAsync<MovieSearchResult>();

        return movies;
    }
}
