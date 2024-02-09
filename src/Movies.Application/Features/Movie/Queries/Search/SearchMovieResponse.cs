using System.Text.Json.Serialization;

namespace Movies.Application.Features.Movie.Queries.Search;
public class SearchMovieResponse
{
    public int TotalRecords { get; set; }
    public List<MovieResponse> Movies { get; set; }

}

public class MovieResponse
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string ImdbId { get; set; }
    public string Type { get; set; }
    public string Poster { get; set; }
}
