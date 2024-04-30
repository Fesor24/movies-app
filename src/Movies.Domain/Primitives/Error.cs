namespace Movies.Domain.Primitives;
public class Error
{
    public Error()
    {
        
    }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public static Error None => new(string.Empty, string.Empty);

    public string Code { get; set; }
    public string Message { get; set; }
}

public class ValidationError : Error
{
    public ValidationError(string code, string message) : base(code, message)
    {
        
    }
}

public class NotFoundError : Error
{
    public NotFoundError(string code, string message) : base(code, message)
    {

    }
}

public class BadRequest : Error
{
    public BadRequest(string code, string message) : base(code, message)
    {

    }
}
