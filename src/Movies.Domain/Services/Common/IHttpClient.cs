using Movies.Domain.Primitives;
using Movies.Domain.Shared;

namespace Movies.Domain.Services.Common;
public interface IHttpClient
{
    Task<Result<TResult, TError>> SendAsync<TRequest, TResult, TError>(HttpRequestMessage<TRequest> request) 
        where TError : Error;

    Task<Result<TResult, TError>> SendAsync<TResult, TError>(Movies.Domain.Shared.HttpRequestMessage request)
        where TError : Error;
}
