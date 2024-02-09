using AutoMapper;
using Movies.Application.Features.Movie.Queries.Search;
using Movies.Domain.Models;

namespace Movies.Application.Mappings;
internal class MovieMapping : Profile
{
    public MovieMapping()
    {
        CreateMap<Movie, MovieResponse>();
    }
}
