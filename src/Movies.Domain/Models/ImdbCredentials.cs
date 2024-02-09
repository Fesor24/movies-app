namespace Movies.Domain.Models;
public class ImdbCredentials
{
    public static readonly string CONFIGURATION = "Imdb";
    public string BaseUrl { get; set; }
    public string ApiKey { get; set; }
}
