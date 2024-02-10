using MediatR;
using Movies.Domain.Primitives;

namespace Movies.Application.Features.Movie.Queries.Search;
public record SearchMovieRequest(string SearchTerm, int Page) : IRequest<Result<SearchMovieResponse, Error>>;
