using Movies.Domain.Models;
using Movies.Domain.Primitives;

namespace Movies.Domain.Services;
public interface IMovieService
{
    Task<Result<MovieSearchResult, Error>> Search(string searchTerm);
    Task<Result<MovieItemSearchResult, Error>> GetMovieByImdbId(string imdbId);
}
