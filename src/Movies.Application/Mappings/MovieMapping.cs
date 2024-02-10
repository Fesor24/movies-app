using AutoMapper;
using Movies.Application.Features.Movie.Queries.GetByImdbId;
using Movies.Application.Features.Movie.Queries.Search;
using Movies.Domain.Models;

namespace Movies.Application.Mappings;
public class MovieMapping : Profile
{
    public MovieMapping()
    {
        CreateMap<Movie, MovieResponse>();
        CreateMap<MovieItemSearchResult, GetMovieResponse>();
        CreateMap<Ratings, MovieRating>();
    }
}
