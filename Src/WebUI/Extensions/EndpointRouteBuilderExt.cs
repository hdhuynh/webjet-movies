namespace Webjet.WebUI.Extensions;

public static class EndpointRouteBuilderExt
{
    /// <summary>
    /// Used for GET endpoints that return one or more items.
    /// </summary>
    public static RouteHandlerBuilder ProducesGet<T>(this RouteHandlerBuilder builder) => builder
        .Produces<T>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status500InternalServerError);
}

public static class WebApplicationExt
{
    public static RouteGroupBuilder MapApiGroup(this WebApplication app, string prefix) => app
        .MapGroup($"api/{prefix}")
        .WithTags(prefix)
        .WithOpenApi();
}