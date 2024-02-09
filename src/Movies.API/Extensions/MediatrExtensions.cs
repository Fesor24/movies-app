using MediatR;
using Movies.Domain.Primitives;

namespace Movies.API.Extensions;

internal static class MediatrExtensions
{
    internal static void MediatorGet<TRequest, TResponse>(this WebApplication app, string endpointGroup, string route)
        where TRequest : IRequest<Result<TResponse, Error>>
    {
        route = "api/" + endpointGroup + route;

        app.MapGet(route, HandleGetRequests<TRequest, TResponse>)
            .WithGroupName(endpointGroup);
    }

    private static async Task<IResult> HandleGetRequests<TRequest, TResponse>([AsParameters] TRequest request, 
        ISender sender)
        where TRequest : IRequest<Result<TResponse, Error>>
    {
        var res = await sender.Send(request);

        return res.Match(res => Results.Ok(res), err => Results.BadRequest(err));
    }
}
