using Movies.API.Abstractions;
using Movies.API.Extensions;
using Movies.Application.Features.Movie.Queries.GetByImdbId;
using Movies.Application.Features.Movie.Queries.Search;

namespace Movies.API.Endpoints;

public class MoviesEndpoint : IEndpointDefinition
{
    public void RegisterEndpoint(WebApplication app)
    {
        const string ENDPOINT = "movies";

        app.MediatorGet<SearchMovieRequest, SearchMovieResponse>(ENDPOINT, "/search");
        app.MediatorGet<GetMovieByImdbIdRequest, GetMovieResponse>(ENDPOINT, "/");
    }
}
