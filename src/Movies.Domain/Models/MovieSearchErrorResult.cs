using Movies.Domain.Primitives;

namespace Movies.Domain.Models;
public class MovieSearchErrorResult : Error
{
    public MovieSearchErrorResult(string code, string message) : base(code, message)
    {
        
    }

    public MovieSearchErrorResult()
    {
        
    }

    public string Response { get; set; }
    public string Error { get; set; }
}

