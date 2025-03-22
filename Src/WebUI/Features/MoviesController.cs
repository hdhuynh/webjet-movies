using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        // group
        //     .MapGet("/{id}",
        //         (int id, ISender sender, CancellationToken ct) => sender.Send(new GetMovieDetailQuery(id), ct))
        //     .WithName("GetMovieDetail")
        //     .ProducesGet<MovieDetailVm>();
    }
}