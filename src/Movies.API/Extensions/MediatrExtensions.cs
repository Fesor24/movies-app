using MediatR;
using Movies.Domain.Primitives;

namespace Movies.API.Extensions;

internal static class MediatrExtensions
{
    internal static void MediatorGet<TRequest, TResponse>(this WebApplication app, string endpointGroup, string route)
        where TRequest : IRequest<Result<TResponse>>
    {
        route = "api/" + endpointGroup + route;

        app.MapGet(route, HandleGetRequests<TRequest, TResponse>)
            .WithGroupName(endpointGroup);
    }

    private static async Task<IResult> HandleGetRequests<TRequest, TResponse>([AsParameters] TRequest request, 
        ISender sender)
        where TRequest : IRequest<Result<TResponse>>
    {
        var res = await sender.Send(request);

        return res.Match(res => Results.Ok(res), HandleError);
    }

    internal static IResult HandleError(Error error)
    {
        if (error.GetType() == typeof(NotFoundError))
            return Results.NotFound(error);
        else if (error.GetType() == typeof(ValidationError))
            return Results.BadRequest(error);
        else
            return Results.BadRequest(error);
    }
}
