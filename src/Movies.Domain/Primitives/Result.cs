namespace Movies.Domain.Primitives;
public class Result
{
    protected Result()
    {
        IsSuccess = true;
        //Error = Error.None;
    }

    protected Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; private set; }
    public bool IsFailure => !IsSuccess;

    public Error Error { get; private set; }

    public static Result Success => new();
    public static Result Failure(Error error) => new(false, error);
}
