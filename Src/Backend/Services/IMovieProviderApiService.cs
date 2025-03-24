using Webjet.Backend.Common.Configuration;
using Webjet.Backend.Movies.GetMovieDetail;
using Webjet.Backend.Movies.GetMovieList;

namespace Webjet.Backend.Services;

public interface IMovieProviderApiService
{
    Task<MoviesListDto> GetAllMovies(MovieProvider movieProvider);
    Task<MovieDetailDto> GetMovieDetails(MovieProvider movieProvider, string movieId);
}