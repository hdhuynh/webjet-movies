using MediatR;
using Webjet.Backend.Movies.GetMovieDetail;
using Webjet.Backend.Movies.GetMovieList;
using Webjet.WebUI.Extensions;

namespace Webjet.WebUI.Features;

public static class MovieEndpoints
{
    public static void MapMovieEndpoints(this WebApplication app)
    {
        var group = app
            .MapApiGroup("movies")
            .AllowAnonymous();

        group
            .MapGet("/", (ISender sender, CancellationToken ct) => sender.Send(new GetMoviesListQuery(), ct))
            .WithName("GetMoviesList")
            .ProducesGet<MoviesListVm>();

        group
            .MapGet("/{movieId}",
                (string movieId, ISender sender, CancellationToken ct) => sender.Send(new GetMovieDetailQuery(movieId), ct))
            .WithName("GetMovieDetail")
            .ProducesGet<MovieDetailVm>();
    }
}