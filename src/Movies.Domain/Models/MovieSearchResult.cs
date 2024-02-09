namespace Movies.Domain.Models;
public class MovieSearchResult
{
    public string TotalResults { get; set; }
    public string Response { get; set; }
    public List<Movie> Search { get; set; }
}

public class Movie
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string ImdbId { get; set; }
    public string Type { get; set; }
    public string Poster { get; set; }
}
