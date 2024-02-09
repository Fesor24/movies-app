namespace Movies.Domain.Primitives;
public class Error
{
    public Error()
    {
        
    }

    public Error(string code, string message, string details)
    {
        Code = code;
        Message = message;
        Details = details;
    }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
        Details = string.Empty;
    }

    public static Error None => new(string.Empty, string.Empty, string.Empty);

    public string Code { get; set; }
    public string Message { get; set; }
    public string Details { get; set; }
}
