using Microsoft.Extensions.Options;
using Movies.Domain.Models;
using Movies.Domain.Primitives;
using Movies.Domain.Services;
using Movies.Domain.Services.Common;

namespace Movies.Infrastructure.Services;
public class MovieService : IMovieService
{
    private readonly IHttpClient _httpClient;
    private readonly ImdbCredentials _credentials;
    private string _url;

    public MovieService(IHttpClient httpClient, IOptions<ImdbCredentials> credentials)
    {
        _httpClient = httpClient;
        _credentials = credentials.Value;
        _url = credentials.Value.BaseUrl + $"?apiKey={credentials.Value.ApiKey}";
    }

    public async Task<Result<MovieItemSearchResult, Error>> GetMovieByImdbId(string imdbId)
    {
        _url += $"&i={imdbId}";

        var response = await _httpClient.SendAsync<MovieItemSearchResult, MovieSearchErrorResult>(
            new Domain.Shared.HttpRequestMessage
            {
                Headers = new Dictionary<string, string>(),
                Method = HttpMethod.Get,
                Uri = _url
            });

        if (response.IsSuccess && response.Value.Response == "True") return response.Value;

        else
        {
            if (response.IsSuccess)
                return new Error(string.Empty, "An error occurred while getting this movie",
                    "Possible reasons: Incorrect Imdb Id");

            if (response.Error is MovieSearchErrorResult errorResult)
            {
                Error err = new(string.Empty, errorResult.Message, errorResult.Error ??= string.Empty);

                return err;
            }

            return response.Error;
        }
    }

    public async Task<Result<MovieSearchResult, Error>> Search(string searchTerm, int page)
    {
        _url += $"&s={searchTerm}&page={page}";

        var response = await _httpClient.SendAsync<MovieSearchResult, MovieSearchErrorResult>
            (new Domain.Shared.HttpRequestMessage
        {
            Headers = new Dictionary<string, string>(),
            Method = HttpMethod.Get,
            Uri = _url
        });

        if (response.IsSuccess && response.Value.Response == "True") return response.Value;

        else
        {
            if (response.IsSuccess)
                return new Error(string.Empty, "An error occurred while searching for this movie");

            if(response.Error is MovieSearchErrorResult errorResult)
            {
                Error err = new(string.Empty, errorResult.Message, errorResult.Error ??= string.Empty);

                return err;
            }

            return response.Error;
        }
    }
}
