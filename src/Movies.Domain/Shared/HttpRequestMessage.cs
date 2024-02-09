namespace Movies.Domain.Shared;
public class HttpRequestMessage
{
    public string Uri { get; set; }
    public Dictionary<string, string> Headers { get; set; } = new();
    public HttpMethod Method { get; set; }
}

public class HttpRequestMessage<TBody> : HttpRequestMessage
{
    public TBody Body { get; set; }
}


