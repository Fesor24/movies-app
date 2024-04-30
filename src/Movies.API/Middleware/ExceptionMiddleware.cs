using Movies.Domain.Primitives;
using System.Net;
using System.Text.Json;

namespace Movies.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var error = _env.IsDevelopment() ?
                new Error("INTERNAL_SERVER_ERROR", $"{ex.Message} \n {ex.StackTrace}") :
                new Error("INTERNAL_SERVER_ERROR", ex.Message);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await context.Response
                .WriteAsync(JsonSerializer.Serialize(error, jsonOptions));
        }
    }
}
