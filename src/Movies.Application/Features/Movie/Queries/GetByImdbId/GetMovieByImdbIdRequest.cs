using MediatR;
using Movies.Domain.Primitives;

namespace Movies.Application.Features.Movie.Queries.GetByImdbId;
public record GetMovieByImdbIdRequest(string ImdbId) : 
    IRequest<Result<GetMovieResponse>>;
