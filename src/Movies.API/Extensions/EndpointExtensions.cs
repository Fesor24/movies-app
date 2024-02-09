using Movies.API.Abstractions;
using System.Reflection;

namespace Movies.API.Extensions;

public static class EndpointExtensions
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        var endpoints = Assembly.GetExecutingAssembly()
            .GetExportedTypes()
            .Where(x => x.IsAssignableTo(typeof(IEndpointDefinition)) && !x.IsAbstract && !x.IsInterface)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDefinition>();

        foreach (var endpoint in endpoints)
        {
            endpoint.RegisterEndpoint(app);
        }
    }
}
