using Movies.Domain.Primitives;
using Movies.Domain.Services.Common;
using Movies.Domain.Shared;
using System.Net.Http.Json;
using System.Text.Json;

namespace Movies.Infrastructure.Services.Common;
public class CustomHttpClient : IHttpClient
{
    private readonly IHttpClientFactory _httpClient;
    public CustomHttpClient(IHttpClientFactory httpClient) => _httpClient = httpClient;

    public async Task<Result<TResult, TError>> SendAsync<TRequest, TResult, TError>(HttpRequestMessage<TRequest> request)
        where TError : Error
    {
        using var client = _httpClient.CreateClient();

        HttpResponseMessage response = default;

        foreach(var header in request.Headers)
        {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        try
        {
            response = await client.SendAsync(new System.Net.Http.HttpRequestMessage
            {
                Content = JsonContent.Create(request.Body),
                Method = request.Method,
                RequestUri = new Uri(request.Uri)
            });
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return await HandleHttpResponse<TResult, TError>(response);
    }

    public async Task<Result<TResult, TError>> SendAsync<TResult, TError>(Domain.Shared.HttpRequestMessage request)
        where TError : Error
    {
        using var client = _httpClient.CreateClient();

        HttpResponseMessage response = default;

        foreach (var header in request.Headers)
        {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        try
        {
            response = await client.SendAsync(new System.Net.Http.HttpRequestMessage
            {
                Method = request.Method,
                RequestUri = new Uri(request.Uri)
            });
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return await HandleHttpResponse<TResult, TError>(response);
    }

    private async Task<Result<TResult, TError>> HandleHttpResponse<TResult, TError>(HttpResponseMessage response)
        where TError : Error
    {
        var jsonOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TResult>(jsonOptions);

            return result;
        }
        else
        {
            var error = await response.Content.ReadFromJsonAsync<TError>(jsonOptions);

            if (error is not null) error.Message = response.ReasonPhrase;

            else
            {
                TError errorInstance = (TError)Activator.CreateInstance(typeof(TError), string.Empty, response.ReasonPhrase);

                return errorInstance;
            }

            return error;
        }
    }
}
