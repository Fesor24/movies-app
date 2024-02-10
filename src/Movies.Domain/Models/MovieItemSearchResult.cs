using System.Text.Json.Serialization;

namespace Movies.Domain.Models;
public class MovieItemSearchResult
{
    public string Title { get; set; }
    public string Year { get; set; }
    public string Rated { get; set; }
    public string Released { get; set; }
    public string Runtime { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public string Writer { get; set; }
    public string Actors { get; set; }
    public string Plot { get; set; }
    public string Language { get; set; }
    public string Country { get; set; }
    public string Awards { get; set; }
    public string Poster { get; set; }
    public List<Ratings> Ratings { get; set; }
    public string Metascore { get; set; }
    [JsonPropertyName("imdbRating")]
    public string ImdbRating { get; set; }
    [JsonPropertyName("imdbVotes")]
    public string ImdbVotes { get; set; }
    [JsonPropertyName("imdbID")]
    public string ImdbId { get; set; }
    public string Type { get; set; }
    [JsonPropertyName("DVD")]
    public string Dvd { get; set; }
    public string BoxOffice { get; set; }
    public string Production { get; set; }
    public string Website { get; set; }
    public string Response { get; set; }

}

public class Ratings
{
    public string Source { get; set; }
    public string Value { get; set; }

}
