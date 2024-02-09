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

    public async Task<Result<MovieSearchResult, Error>> Search(string searchTerm)
    {
        _url += $"&s={searchTerm}";

        var response = await _httpClient.SendAsync<MovieSearchResult, MovieSearchErrorResult>
            (new Domain.Shared.HttpRequestMessage
        {
            Headers = new Dictionary<string, string>(),
            Method = HttpMethod.Get,
            Uri = _url
        });

        if (response.IsSuccess) return response.Value;

        else
        {
            if(response.Error is MovieSearchErrorResult errorResult)
            {
                Error err = new(string.Empty, errorResult.Message, errorResult.Error ??= string.Empty);

                return err;
            }

            return response.Error;
        }
    }
}
