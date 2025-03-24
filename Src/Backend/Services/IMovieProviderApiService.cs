using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Services.Movies.GetMovieDetail;
using Webjet.Backend.Services.Movies.GetMovieList;

namespace Webjet.Backend.Services;

public interface IMovieProviderApiService
{
    Task<MoviesListDto> GetAllMovies(MovieProvider movieProvider);
    Task<MovieDetailDto> GetMovieDetails(MovieProvider movieProvider, string movieId);
}