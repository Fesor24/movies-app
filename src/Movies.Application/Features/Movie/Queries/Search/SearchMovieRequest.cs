using MediatR;
using Movies.Domain.Primitives;

namespace Movies.Application.Features.Movie.Queries.Search;
public record SearchMovieRequest(string SearchTerm) : IRequest<Result<SearchMovieResponse, Error>>;
