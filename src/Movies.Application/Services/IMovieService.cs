using Movies.Domain.Models;
using Movies.Domain.Primitives;

namespace Movies.Application.Services;
public interface IMovieService
{
    Task<Result<MovieItemSearchResult>> GetMovieByImdbId(string id);

    Task<Result<MovieSearchResult>> Search(string searchTerm, int page);
}
