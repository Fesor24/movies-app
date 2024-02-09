namespace Movies.API.Abstractions;

public interface IEndpointDefinition
{
    void RegisterEndpoint(WebApplication app);
}
