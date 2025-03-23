using AutoMapper;
using MediatR;
using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Common.Interfaces;
using Webjet.Backend.Models.Data;
using Webjet.Backend.Services;

namespace Webjet.Backend.Movies.GetMovieList;

public record GetMoviesListQuery : IRequest<MoviesListVm>;

public class GetMoviesListQueryHandler(IMovieProviderApiService movieProviderApiService) : IRequestHandler<GetMoviesListQuery, MoviesListVm>
{
    public async Task<MoviesListVm> Handle(GetMoviesListQuery request, CancellationToken cancellationToken)
    {
        var moviesListDto = await movieProviderApiService.GetAllMovies(MovieProvider.CinemaWorld);
        return new MoviesListVm
        {
            Movies = moviesListDto.Movies

        };
    }
}